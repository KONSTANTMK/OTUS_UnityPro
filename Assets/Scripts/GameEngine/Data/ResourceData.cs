using System;
using UnityEngine;

namespace SaveSystem.Data
{
    [Serializable]
    public struct ResourceData
    {
        [SerializeField]
        public string id;

        [SerializeField]
        public int amount;

        public ResourceData(string id, int amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
}