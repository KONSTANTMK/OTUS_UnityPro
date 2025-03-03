using System.Collections.Generic;
using Zenject;

namespace GameEngine.DI
{
    public class SceneInstaller : MonoInstaller
    {
        
        public override void InstallBindings()
        {
            Container.Bind<MoneyStorage>().FromComponentInHierarchy().AsSingle();
        }
    }  
}