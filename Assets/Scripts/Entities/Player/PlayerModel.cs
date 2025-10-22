using System;

namespace Homework.EventBus
{
    public sealed class PlayerModel : DeclarativeModel
    {
        [Section]
        public Position position;

        [Section]
        public Stats stats;

        [Section]
        public Weapon Weapon;
    }

    [Serializable]
    public class Weapon
    {
        public WeaponConfig WeaponConfig;
    }
}