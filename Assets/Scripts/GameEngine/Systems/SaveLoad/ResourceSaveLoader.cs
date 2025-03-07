using UnityEngine;

namespace GameEngine
{
    public class ResourceSaveLoader :  ISaveLoader
    {
        void ISaveLoader.SaveGame(GameContext gameContext, IGameRepository gameRepository)
        {
            var moneyStorage = gameContext.MoneyStorage;
            gameRepository.SetData(moneyStorage.Money);
            Debug.Log($"Money saved: {moneyStorage.Money}");
        }

        void ISaveLoader.LoadGame(GameContext gameContext, IGameRepository gameRepository)
        {
            var moneyStorage = gameContext.MoneyStorage;
            if (gameRepository.TryGetData(out int money))
            {
                moneyStorage.SetupMoney(money);
                Debug.Log($"Money loaded: {money}");
            }
            else
            {
                Debug.Log($"Money not loaded");
            }
        }
    }
}