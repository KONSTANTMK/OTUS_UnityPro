using System;
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
            
            var savePath = Path.Combine(Application.persistentDataPath, "save.json");
            
            try
            {
                File.WriteAllText(savePath, contents: encryptedJson);
                Debug.Log(message: "Successfully Saved");
            }
            catch (Exception ex)
            {
                Debug.Log(message: "Save Failed"+ex);
            }
        }

        public void LoadState()
        {
            var savePath = Path.Combine(Application.persistentDataPath, "save.json");
            
            if (!File.Exists(savePath))
            {
                Debug.Log(message: "Save File Not Found");
                return;
            }

            try
            {
                string encryptedJson = File.ReadAllText(savePath);
                
                byte[] encryptedData = JsonConvert.DeserializeObject<EncryptedSaveStruct>(encryptedJson).Data;
                
                var gameStateJson = encryptComponent.Decrypt(encryptedData);
                
                gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(gameStateJson);
            }

            catch (Exception)
            {
                Debug.Log(message: "Save Data Not Read");
            }
        }
        
    }
}