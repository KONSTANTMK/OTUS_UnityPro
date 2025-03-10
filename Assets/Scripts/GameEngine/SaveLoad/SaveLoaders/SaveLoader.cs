using UnityEngine;

namespace GameEngine.SaveLoad
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

        protected virtual void SetupDefaultData(TService service, TData data) {}
        protected abstract TData ConvertToData(TService service);
        protected abstract void SetupData(TService service,TData data);
    }
}