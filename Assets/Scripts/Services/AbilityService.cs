using Zenject;
using UnityEngine;
using Core.EventBus;
using Game;
using State;
using System.Collections.Generic;

namespace Services
{
    public class AbilityService : IInitializable
    {
        private readonly BoardState _board;
        private readonly RandomService _rng;

        public AbilityService(BoardState board, RandomService rng)
        {
            _board = board;
            _rng = rng;
        }

        public void Initialize()
        {
            Debug.Log("[AbilityService] Init");
            EventBus.Subscribe<AttackRequested>(OnAttackRequested_Preprocess);  // Орк, Ледмаг
            EventBus.Subscribe<DamageApplied>(OnDamageApplied_Reactive);        // Электро, Вамп
            EventBus.Subscribe<TurnEnded>(OnTurnEnded_Triggers);                // Пожиратель, Медитатор
        }

        // ---------- 1) ДО боя: вмешательство в атаку ----------
        private void OnAttackRequested_Preprocess(AttackRequested e)
        {
            var attacker = Find(e.AttackerIndex);
            var target   = Find(e.TargetIndex);
            if (attacker == null || target == null || !attacker.Alive || !target.Alive) return;

            // Орк: 50% — сменить цель на случайного другого врага
            if (attacker.Kind == HeroKind.DumbOrc && _rng.Roll50())
            {
                var enemies = GetAliveOpponents(attacker.Team);
                if (enemies.Count > 1)
                {
                    var newTarget = PickRandom(exceptIndex: target.Index, from: enemies);
                    if (newTarget != null)
                    {
                        Debug.Log($"[Ability] Orc switches {target.Name} -> {newTarget.Name}");
                        EventBus.Publish(new AttackRequested { AttackerIndex = attacker.Index, TargetIndex = newTarget.Index });
                        return; // прерываем текущую атаку — пойдёт новая с изменённой целью
                    }
                }
            }

            // Ледмаг: при атаке замораживает цель (пропустит 1 свой ход)
            if (attacker.Kind == HeroKind.IceMage && target.Alive)
            {
                if (!target.Frozen) // чтобы не спамить
                {
                    target.Frozen = true;
                    EventBus.Publish(new FreezeApplied { Index = target.Index });
                    Debug.Log($"[Ability] Ice freeze on {target.Name}");
                }
            }
            // дальше событие пойдёт в CombatService как обычно
        }

        // ---------- 2) ПОСЛЕ урона: реактивные эффекты ----------
        private void OnDamageApplied_Reactive(DamageApplied e)
        {
            var src = Find(e.SourceIndex);
            var dst = Find(e.TargetIndex);
            if (dst == null) return;

            // Электро: когда получает урон — всем остальным -1 (без каскадов)
            if (dst.Kind == HeroKind.Electro && e.Amount > 0)
            {
                foreach (var h in AllAlive())
                {
                    if (h.Index == dst.Index) continue;
                    ApplyPing(h, 1);
                }
                Debug.Log("[Ability] Electro pulse -1 to all");
            }

            // Вамп: после СВОЕЙ атаки (не контрудар), 50% — лечится на нанесённый урон
            if (src != null && !e.IsRetaliation && src.Kind == HeroKind.LordVamp && e.Amount > 0 && _rng.Roll50())
            {
                Heal(src, e.Amount);
                Debug.Log($"[Ability] Vamp heals {src.Name} for {e.Amount}");
            }
        }

        // ---------- 3) В КОНЦЕ СВОЕГО ХОДА ----------
        private void OnTurnEnded_Triggers(TurnEnded e)
        {
            var me = Find(e.ActiveIndex);
            if (me == null || !me.Alive) return;

            // Пожиратель: -3 по случайному врагу
            if (me.Kind == HeroKind.Devourer)
            {
                var victim = PickRandom(from: GetAliveOpponents(me.Team));
                if (victim != null)
                {
                    ApplyDirectDamage(victim, 3);
                    Debug.Log($"[Ability] Devourer hits {victim.Name} for 3");
                }
            }

            // Медитатор: +1 к случайному союзнику
            if (me.Kind == HeroKind.Meditator)
            {
                var ally = PickRandom(from: GetAliveAllies(me.Team));
                if (ally != null)
                {
                    Heal(ally, 1);
                    Debug.Log($"[Ability] Meditator heals {ally.Name} for 1");
                }
            }
        }

        // ================= Helpers =================
        private Hero Find(int index)
        {
            foreach (var h in _board.Red)  if (h.Index == index) return h;
            foreach (var h in _board.Blue) if (h.Index == index) return h;
            return null;
        }

        private List<Hero> GetAliveOpponents(TeamId myTeam)
            => myTeam == TeamId.Red ? _board.Blue.FindAll(h => h.Alive) : _board.Red.FindAll(h => h.Alive);

        private List<Hero> GetAliveAllies(TeamId myTeam)
            => myTeam == TeamId.Red ? _board.Red.FindAll(h => h.Alive) : _board.Blue.FindAll(h => h.Alive);

        private IEnumerable<Hero> AllAlive()
        {
            foreach (var h in _board.Red)  if (h.Alive) yield return h;
            foreach (var h in _board.Blue) if (h.Alive) yield return h;
        }

        private Hero PickRandom(List<Hero> from, int? exceptIndex = null)
        {
            var pool = (exceptIndex.HasValue) ? from.FindAll(h => h.Index != exceptIndex.Value) : from;
            if (pool.Count == 0) return null;
            return pool[_rng.Range(0, pool.Count)];
        }

        private void ApplyPing(Hero target, int amount)
        {
            var old = target.Health;
            target.Health = Mathf.Max(0, target.Health - amount);
            EventBus.Publish(new DamageApplied { SourceIndex = -1, TargetIndex = target.Index, Amount = amount, IsRetaliation = false });
            EventBus.Publish(new HealthChanged { Index = target.Index, OldHp = old, NewHp = target.Health });
            if (target.Health <= 0 && target.Alive) { target.Alive = false; EventBus.Publish(new HeroDied { Index = target.Index }); }
        }

        private void ApplyDirectDamage(Hero target, int amount) => ApplyPing(target, amount);

        private void Heal(Hero h, int val)
        {
            var old = h.Health;
            h.Health = Mathf.Min(h.MaxHealth, h.Health + val);
            EventBus.Publish(new HealthChanged { Index = h.Index, OldHp = old, NewHp = h.Health });
        }
    }
}
