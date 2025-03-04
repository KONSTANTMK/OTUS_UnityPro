using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace GameEngine
{
    public class GameRepository : IGameRepository
    {
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
            PlayerPrefs.SetString(GAME_STATE_KEY, gameStateJson);
        }

        public void LoadState()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                var gameStateJson = PlayerPrefs.GetString(GAME_STATE_KEY);
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