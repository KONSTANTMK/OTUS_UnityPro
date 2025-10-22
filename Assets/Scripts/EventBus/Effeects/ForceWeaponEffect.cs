namespace Homework.EventBus
{
    public class ForceWeaponEffect : IWeaponEffect
    {
        public IEntity Source { get; set; }
        public IEntity Target { get; set; }
    }
}