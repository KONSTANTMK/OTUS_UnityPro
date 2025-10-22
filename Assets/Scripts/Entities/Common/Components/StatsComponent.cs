namespace Homework.EventBus
{
    public sealed class StatsComponent
    {
        public int Strength => _stats.strength.Value;
        
        private readonly Stats _stats;
        
        public StatsComponent(Stats stats)
        {
            _stats = stats;
        }
    }
}