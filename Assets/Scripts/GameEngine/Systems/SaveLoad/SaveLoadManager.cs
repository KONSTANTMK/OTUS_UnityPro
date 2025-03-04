using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameEngine
{
    public class SaveLoadManager:MonoBehaviour
    {
        private IEnumerable<ISaveLoader> saveLoaders;
        private GameContext gameContext;
        
        [Inject]
        public void Construct(GameContext gameContext, IEnumerable<ISaveLoader> saveLoaders) 
        {
            this.gameContext = gameContext;
            this.saveLoaders = saveLoaders;
        }
        
        [Button]
        public void Load()
        {
            foreach (var saveLoader in saveLoaders)
            {
                saveLoader.LoadGame(gameContext);
            }
        }
        
        [Button]
        public void Save()
        {
            foreach (var saveLoader in saveLoaders)
            {
                saveLoader.SaveGame(gameContext);
            }
        }
    }
}