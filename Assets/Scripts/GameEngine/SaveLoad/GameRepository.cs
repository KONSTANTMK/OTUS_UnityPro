using System.Collections.Generic;
using System.IO;
using GameEngine.AES;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace GameEngine.SaveLoad
{
    public class GameRepository : IGameRepository
    {
        [Inject]
        private AesEncryptComponent encryptComponent;
        private const string GAME_STATE_KEY = "GameStateKey";
        private Dictionary<string, string> gameState = new();
        
        public void SetData<T>(T data)
        {
            var key = typeof(T).ToString(); 
            var jsonData = JsonConvert.SerializeObject(data);
            gameState[key] = jsonData;
        }
        
        public bool TryGetData<T>(out T data)
        {
            var key = typeof(T).ToString();
            if (gameState.TryGetValue(key, out var jsonData))
            {
                data = JsonConvert.DeserializeObject<T>(jsonData);
                return true;
            }
            data = default;
            return false;
        }

        public void SaveState()
        {
            var gameStateJson = JsonConvert.SerializeObject(gameState);
            
            byte[] encryptedData = encryptComponent.Encrypt(gameStateJson);
            
            EncryptedSaveStruct encryptedSaveStruct = new()
            {
                Data = encryptedData
            };
            
            string encryptedJson = JsonConvert.SerializeObject(encryptedSaveStruct);
            
            PlayerPrefs.SetString(GAME_STATE_KEY, encryptedJson);
        }

        public void LoadState()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                var encryptedJson = PlayerPrefs.GetString(GAME_STATE_KEY);
                
                byte[] encryptedData = JsonConvert.DeserializeObject<EncryptedSaveStruct>(encryptedJson).Data;
                
                var gameStateJson = encryptComponent.Decrypt(encryptedData);
                
                gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(gameStateJson);
                Debug.Log($"Game state loaded: {gameStateJson}");
            }
            else
            {
                Debug.Log($"Game state not loaded");
            }
        }
        
    }
}