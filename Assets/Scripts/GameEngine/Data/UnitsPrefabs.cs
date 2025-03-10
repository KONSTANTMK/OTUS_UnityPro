using System.Collections.Generic;
using GameEngine.Objects;
using UnityEngine;

namespace SaveSystem.Data
{
    [CreateAssetMenu(fileName = "UnitsPrefabs", menuName = "UnitsPrefabs", order = 0)]
    public class UnitsPrefabs : ScriptableObject
    {
        [SerializeField] private List<Unit>  units = new ();
        
        public IReadOnlyList<Unit> Units => units;
    }
}