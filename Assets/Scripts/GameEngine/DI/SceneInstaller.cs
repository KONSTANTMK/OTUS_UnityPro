using System.Collections.Generic;
using System.Linq;
using GameEngine.Objects;
using GameEngine.SaveLoad;
using GameEngine.Services;
using SaveSystem.Data;
using UnityEngine;
using Zenject;

namespace GameEngine.DI
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private UnitsPrefabs unitsPrefabs;
        private IEnumerable<Resource> resources;
        private IEnumerable<Unit> units;
        public override void InstallBindings()
        {
            Container.Bind<UnitsPrefabs>().FromInstance(unitsPrefabs).AsCached().NonLazy();
            
            resources = FindObjectsOfType<MonoBehaviour>(true).OfType<Resource>().ToList();
            Container.Bind<IEnumerable<Resource>>().FromInstance(resources).AsCached().NonLazy();
            Container.Bind<ResourceService>().FromNew().AsCached().NonLazy();
            
            units = FindObjectsOfType<MonoBehaviour>(true).OfType<Unit>().ToList();
            Container.Bind<IEnumerable<Unit>>().FromInstance(units).AsCached().NonLazy();
            Container.Bind<UnitService>().FromNew().AsCached().NonLazy();
            
            Container.Bind<GameContext>().AsSingle();
            
            Container.Bind<ISaveLoader>().To<ResourceSaveLoader>().AsCached().NonLazy();
            Container.Bind<ISaveLoader>().To<UnitsSaveLoader>().AsCached().NonLazy();
            
            Container.Bind<GameRepository>().AsSingle();
            
            
        }
    }  
}