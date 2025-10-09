using Core.EventBus;

namespace Game
{
    // Запуск матча
    public readonly struct StartGame : IGameEvent { }

    // Ход
    public struct TurnStarted : IGameEvent { public int ActiveIndex { get; set; } }
    public struct TurnEnded   : IGameEvent { public int ActiveIndex { get; set; } }

    // Боевая логика
    public struct AttackRequested : IGameEvent
    {
        public int AttackerIndex { get; set; }
        public int TargetIndex   { get; set; }
    }

    public struct DamageApplied : IGameEvent
    {
        public int SourceIndex   { get; set; }
        public int TargetIndex   { get; set; }
        public int Amount        { get; set; }
        public bool IsRetaliation{ get; set; }
    }

    public struct HealthChanged : IGameEvent
    {
        public int Index  { get; set; }
        public int OldHp  { get; set; }
        public int NewHp  { get; set; }
    }

    public struct HeroDied : IGameEvent
    {
        public int Index { get; set; }
    }

    // Эффекты/статусы
    public struct FreezeApplied  : IGameEvent { public int Index { get; set; } } // пропустит 1 свой ход
    public struct ShieldConsumed : IGameEvent { public int Index { get; set; } } // паладин потерял щит

    // Завершение
    public struct GameEnded : IGameEvent { public int WinnerTeam { get; set; } }

    // UI/SFX (чтобы не звать Audio напрямую)
    public struct PlaySfx : IGameEvent { public string Key { get; set; } }
    
    public struct EndTurnRequested : IGameEvent { }
}