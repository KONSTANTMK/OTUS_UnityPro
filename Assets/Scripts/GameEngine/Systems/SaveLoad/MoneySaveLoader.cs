using System;
using UnityEngine;

namespace GameEngine
{
    
    public abstract class SaveLoader<TService,TData> : ISaveLoader
    {
        void ISaveLoader.SaveGame(GameContext gameContext, IGameRepository gameRepository)
        {
            TService service = gameContext.GetService<TService>();
            TData data = ConvertToData(service);
            gameRepository.SetData(data);
            Debug.Log($"<color=yellow>Saved data{data.GetType().Name}</color>");
        }

        void ISaveLoader.LoadGame(GameContext gameContext, IGameRepository gameRepository)
        {
            TService service = gameContext.GetService<TService>();
            if (gameRepository.TryGetData(out TData data))
            {
                SetupData(service, data);
                Debug.Log($"<color=yellow>Loaded data{data.GetType().Name}</color>");
            }
            else
            {
                SetupDefaultData(service, data);
                Debug.Log($"<color=pink>Data not loaded{data.GetType().Name}</color>");
            }
        }

        protected virtual void SetupDefaultData(TService service, TData data)
        {
            
        }
        protected abstract TData ConvertToData(TService service);
        protected abstract void SetupData(TService service,TData data);
    }

    [Serializable]
    public class MoneyData
    {
        public int Money;
    }
    
    public class MoneySaveLoader : SaveLoader<MoneyStorage, MoneyData>
    {
        protected override MoneyData ConvertToData(MoneyStorage service)
        {
            return new MoneyData()
            {
                Money = service.Money,
            };
        }

        protected override void SetupData(MoneyStorage service, MoneyData data)
        {
            service.SetupMoney(data.Money);
        }
    }
    
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