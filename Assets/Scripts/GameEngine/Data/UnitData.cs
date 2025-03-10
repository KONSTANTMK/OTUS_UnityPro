using System;
using UnityEngine;

namespace GameEngine.Data
{
    [Serializable]
    public struct UnitData
    {
        [SerializeField] public string name;

        [SerializeField] public string type;

        [SerializeField] public int hitPoints;

        [SerializeField] public float xPosition;
        [SerializeField] public float yPosition;
        [SerializeField] public float zPosition;
        [SerializeField] public float xRotation;
        [SerializeField] public float yRotation;
        [SerializeField] public float zRotation;

        public UnitData(string name, string type, int hitPoints, Vector3 position, Vector3 rotation)
        {
            this.name = name;
            this.type = type;

            this.hitPoints = hitPoints;

            xPosition = position.x;
            yPosition = position.y;
            zPosition = position.z;

            xRotation = rotation.x;
            yRotation = rotation.y;
            zRotation = rotation.z;
        }
    }
}