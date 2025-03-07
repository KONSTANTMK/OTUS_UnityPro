using System;
using System.Collections.Generic;
using Zenject;

namespace GameEngine
{
    public class GameContext
    {
        public MoneyStorage MoneyStorage { get; private set; }

        private ISaveLoader[] SaveLoaders;

        [Inject]
        public void Construct(MoneyStorage moneyStorage, ISaveLoader[] saveLoaders)
        {
            MoneyStorage = moneyStorage;
            SaveLoaders = saveLoaders;
        }

        internal object GetService(Type type)
        {
            for (int i = 0, count = SaveLoaders.Length; i < count; i++)
            {
                var currentService = SaveLoaders[i];
                var currentType = currentService.GetType(); 
                
                if (type.IsAssignableFrom(currentType))
                {
                    return currentService;
                }
            }

            throw new Exception($"Service {type.Name} is not found!");
        }
    }
}