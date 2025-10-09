using UnityEngine;
using Core.EventBus;
using State;
using Zenject;

namespace Game
{
    public class TempAttackInput : MonoBehaviour
    {
        private int _activeIndex = -1;
        [Inject]
        public BoardState Board;

        private void OnEnable()
        {
            EventBus.Subscribe<TurnStarted>(OnTurnStarted);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<TurnStarted>(OnTurnStarted);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) && _activeIndex >= 0)
            {
                var attacker = Find(_activeIndex);
                var target   = PickEnemyAlive(attacker?.Team);
                if (attacker != null && target != null)
                    EventBus.Publish(new AttackRequested { AttackerIndex = attacker.Index, TargetIndex = target.Index });
            }
        }

        private void OnTurnStarted(TurnStarted e) => _activeIndex = e.ActiveIndex;

        private Hero Find(int idx)
        {
            foreach (var h in Board.Red)  if (h.Index == idx) return h;
            foreach (var h in Board.Blue) if (h.Index == idx) return h;
            return null;
        }

        private Hero PickEnemyAlive(TeamId? team)
        {
            if (team == null) return null;
            var list = team == TeamId.Red ? Board.Blue : Board.Red;
            // простой выбор первого живого (пока без рандома)
            foreach (var h in list) if (h.Alive) return h;
            return null;
        }
    }
}