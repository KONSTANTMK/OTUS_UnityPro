using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameEngine.DI
{
    public class SceneInstaller : MonoInstaller
    {
        
        public override void InstallBindings()
        {
            Container.Bind<MoneyStorage>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameContext>().AsSingle();
            Container.Bind<ISaveLoader>().To<MoneySaveLoader>().FromNew().AsSingle();
        }
    }  
}