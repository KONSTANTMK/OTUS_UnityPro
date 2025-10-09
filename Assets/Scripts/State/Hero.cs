namespace State
{
    public enum TeamId { Red = 0, Blue = 1 }
    
    public enum HeroKind { Devourer, Huntress, DumbOrc, LordVamp, Paladin, IceMage, Meditator, Electro }

    public class Hero
    {
        public int Index;            // 0..7, уникальный id в матче
        public TeamId Team;          // Red / Blue
        public string Name;          // для логов/отладки
        public bool Alive = true;    // позже будем ставить в false при смерти
        public bool Frozen = false;  // если true — пропустит ровно один свой ход
        public int Attack = 2;
        public int MaxHealth = 10;
        public int Health = 10;
        public HeroKind Kind;
        public bool DivineShield = false; // Паладин: первый получаемый урон игнорируется
    }
    
    
}