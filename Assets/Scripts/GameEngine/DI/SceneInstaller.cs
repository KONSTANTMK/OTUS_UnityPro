using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GameEngine.DI
{
    public class SceneInstaller : MonoInstaller
    {
        private IEnumerable<Resource> resources;
        public override void InstallBindings()
        {
            resources = FindObjectsOfType<MonoBehaviour>(true).OfType<Resource>().ToList();
            Container.Bind<IEnumerable<Resource>>().FromInstance(resources).AsCached().NonLazy();
            Container.Bind<ResourceService>().FromNew().AsCached().NonLazy();
            
            Container.Bind<MoneyService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameContext>().AsSingle();
            Container.Bind<ISaveLoader>().To<MoneySaveLoader>().FromNew().AsSingle();
            Container.Bind<ISaveLoader>().To<ResourceSaveLoader>().AsCached().NonLazy();
            Container.Bind<GameRepository>().AsSingle();
            
            
        }
    }  
}