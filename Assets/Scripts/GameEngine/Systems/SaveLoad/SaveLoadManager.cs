using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    public class SaveLoadManager:MonoBehaviour
    {
        private ISaveLoader saveLoader;

        public void Awake()
        {
            saveLoader = new MoneySaveLoader();
        }

        [Button]
        public void Load()
        {
            var gameContext = gameObject.GetComponent<GameContext>();
            saveLoader.LoadGame(gameContext);
        }
        
        [Button]
        public void Save()
        {
            var gameContext = gameObject.GetComponent<GameContext>();
            saveLoader.SaveGame(gameContext);
        }
    }
}