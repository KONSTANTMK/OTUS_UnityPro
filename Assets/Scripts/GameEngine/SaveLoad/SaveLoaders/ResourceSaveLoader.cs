using System.Collections.Generic;
using System.Linq;
using GameEngine.Data;
using GameEngine.Objects;
using GameEngine.Services;

namespace GameEngine.SaveLoad
{
    public class ResourceSaveLoader : SaveLoader<ResourceService, ResourceData[]>
    {
        protected override ResourceData[] ConvertToData(ResourceService service)
        {
            var resources = service.GetResources();
            
            var resourceArray = resources.ToArray();
            
            ResourceData[] dataArray = new ResourceData[resourceArray.Length];

            for (int i = 0; i < resourceArray.Length; i++)
            {
                var resource = resourceArray[i];
                dataArray[i] = new ResourceData
                {
                    id = resource.ID,
                    amount = resource.Amount
                };
            }

            return dataArray;
        }
        

        override protected void SetupData(ResourceService service, ResourceData[] dataArray)
        {
            IEnumerable<Resource> resources = service.GetResources();

            var enumerable = resources as Resource[] ?? resources.ToArray();
            foreach (var resource in enumerable)
            {
                var data = dataArray.FirstOrDefault(d => d.id == resource.ID);
                resource.Amount = data.amount;
            }
            
            service.SetResources(enumerable);
        }
    }
}