using UnityEngine;

namespace Homework.EventBus
{
    [CreateAssetMenu(
        fileName = "WeaponConfig",
        menuName = "Homework/EventBus/New WeeaponConfig"
    )]

    public class WeaponConfig : ScriptableObject
    {
        [SerializeReference]
        public List<IWeaponEffect> Effects;
    }
}