namespace State
{
    public enum TeamId { Red = 0, Blue = 1 }

    public class Hero
    {
        public int Index;            // 0..7, уникальный id в матче
        public TeamId Team;          // Red / Blue
        public string Name;          // для логов/отладки
        public bool Alive = true;    // позже будем ставить в false при смерти
        public bool Frozen = false;  // если true — пропустит ровно один свой ход
    }
}