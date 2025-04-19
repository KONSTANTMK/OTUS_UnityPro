using System;
using System.Collections.Generic;
using Codice.CM.Common.Merge;
using GameEngine.Objects;
using GameEngine.Services;
using Zenject;

namespace GameEngine
{
    public class GameContext{
    
        
        private readonly List<object> services = new ();
        
        private IEnumerable<Resource> resources;
        private ResourceService resourceService;
        
        private IEnumerable<Unit> units;
        private UnitService unitService;

        [Inject]
        public void Construct(ResourceService resourceService, IEnumerable<Resource> resources, UnitService unitService,IEnumerable<Unit> units)
        {
           this.unitService = unitService;
           this.units = units;
           this.unitService.SetupUnits(this.units);
           services.Add(unitService);
           
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