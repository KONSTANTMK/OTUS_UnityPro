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
        private GameRepository gameRepository;
        
        [Inject]
        public void Construct(GameContext gameContext, IEnumerable<ISaveLoader> saveLoaders, GameRepository gameRepository) 
        {
            this.gameContext = gameContext;
            this.saveLoaders = saveLoaders;
            this.gameRepository = gameRepository;
        }
        
        [Button]
        public void SaveGame()
        {
            foreach (var saveLoader in saveLoaders)
            {
                saveLoader.SaveGame(gameContext,gameRepository);
            }
            gameRepository.SaveState();
        }

        [Button]
        public void LoadGame()
        {
            gameRepository.LoadState();
            
            foreach (var saveLoader in saveLoaders)
            {
                saveLoader.LoadGame(gameContext, gameRepository);
            }
        }
    }
}