using System;
using System.Collections.Generic;
using Zenject;

namespace GameEngine
{
    public class GameContext
    {
        public MoneyService moneyService;

        //private ISaveLoader[] SaveLoaders;
        private readonly List<object> services = new ();
        
        private IEnumerable<Resource> resources;
        private ResourceService resourceService;

        [Inject]
        public void Construct(MoneyService moneyService, ResourceService resourceService, IEnumerable<Resource> resources)
        {
            this.moneyService = moneyService;
            services.Add(moneyService);
           // SaveLoaders = saveLoaders;
           
           
           
            this.resourceService = resourceService;
            this.resources = resources;
            this.resourceService.SetResources(this.resources);
            services.Add(resourceService);
        }

        public T GetService<T>()
        {
            for (int i = 0, count = services.Count; i < count; i++)
            {
                if (services[i] is T result)
                {
                    return result;
                }
            }
            
            throw new Exception($"Service {typeof(T).Name} is not found!");
        }
    }
}