namespace Homework.EventBus
{
    public class IWeaponEffect
    {
        IEntity Source { get; set; }
        IEntity Target { get; set; }
    }
}