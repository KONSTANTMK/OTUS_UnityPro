using Zenject;
using UnityEngine;
using Core.EventBus;
using Game;
using State;

namespace Services
{
    public class CombatService : IInitializable
    {
        private readonly BoardState _board;

        public CombatService(BoardState board) => _board = board;

        public void Initialize()
        {
            Debug.Log("[CombatService] Init");
            EventBus.Subscribe<AttackRequested>(OnAttackRequested);
        }

         void OnAttackRequested(AttackRequested e)
        {
            var attacker = FindByIndex(e.AttackerIndex);
            var target   = FindByIndex(e.TargetIndex);
            if (attacker == null || target == null) return;
            if (!attacker.Alive || !target.Alive)  return;

            Debug.Log($"[CombatService] {attacker.Name} attacks {target.Name}");

            // 1) Паладинский щит — поглощает первый урон (без изменения HP)
            if (target.DivineShield)
            {
                target.DivineShield = false; // щит сгорел
                EventBus.Publish(new ShieldConsumed { Index = target.Index });
                Debug.Log($"[CombatService] Divine shield consumed on {target.Name}");
            }
            else
            {
                // 2) Обычный урон
                var dmg  = Mathf.Max(0, attacker.Attack);
                var old  = target.Health;
                target.Health = Mathf.Max(0, target.Health - dmg);

                EventBus.Publish(new DamageApplied
                {
                    SourceIndex    = attacker.Index,
                    TargetIndex    = target.Index,
                    Amount         = dmg,
                    IsRetaliation  = false
                });
                EventBus.Publish(new HealthChanged
                {
                    Index = target.Index,
                    OldHp = old,
                    NewHp = target.Health
                });

                if (target.Health <= 0 && target.Alive)
                {
                    target.Alive = false;
                    EventBus.Publish(new HeroDied { Index = target.Index });
                    Debug.Log($"[CombatService] {target.Name} died");
                    return; // цель умерла — контрудара не будет
                }
            }

            // 3) Контрудар: НЕТ, если атакующий — Huntress
            bool attackerIsHuntress = attacker.Kind == HeroKind.Huntress;
            if (!attackerIsHuntress && target.Alive)
            {
                Debug.Log($"[CombatService] {target.Name} retaliates {attacker.Name}");

                var rdmg  = Mathf.Max(0, target.Attack);
                var rold  = attacker.Health;
                attacker.Health = Mathf.Max(0, attacker.Health - rdmg);

                EventBus.Publish(new DamageApplied
                {
                    SourceIndex    = target.Index,
                    TargetIndex    = attacker.Index,
                    Amount         = rdmg,
                    IsRetaliation  = true
                });
                EventBus.Publish(new HealthChanged
                {
                    Index = attacker.Index,
                    OldHp = rold,
                    NewHp = attacker.Health
                });

                if (attacker.Health <= 0 && attacker.Alive)
                {
                    attacker.Alive = false;
                    EventBus.Publish(new HeroDied { Index = attacker.Index });
                    Debug.Log($"[CombatService] {attacker.Name} died (retaliation)");
                }
            }
        }


        private Hero FindByIndex(int index)
        {
            foreach (var h in _board.Red)
                if (h.Index == index)
                    return h;
            foreach (var h in _board.Blue)
                if (h.Index == index)
                    return h;
            return null;
        }
    }
}