using System;
using Homework.EventBus;
using Zenject;
using State;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.Bind<EventBus>().AsSingle();
            ConfigureLevel();
            ConfigurePlayer();
            ConfigureControllers();
            ConfigurePipelines();
            
            Container.Bind<EntityInstaller>().FromComponentInHierarchy().AsSingle();
    
        }

        private void ConfigureLevel()
        {
            Container.Bind<TileMap>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EntityMap>().AsSingle();
            Container.Bind<LevelMap>().AsSingle();
        }

        private void ConfigurePlayer()
        {
            Container.Bind<KeyboardInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerService>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputTask>().AsSingle().NonLazy();
        }

        private void ConfigureControllers()
        {
            Container.Bind<ApplyDirectionHandler>().AsSingle();
            Container.Bind<ForceDirectionEvent>().AsSingle();
            Container.Bind<AttackHandler>().AsSingle();
            Container.Bind<DealDamageHandler>().AsSingle();
            Container.Bind<DestroyHandler>().AsSingle();
            Container.Bind<MoveHandler>().AsSingle();
            
            Container.Bind<MoveVisualHandler>().AsSingle();
            Container.Bind<DestroyVisualHandler>().AsSingle();
        }
        
        private void ConfigurePipelines()
        {
            Container.Bind<TurnPipeline>().AsSingle();
            
            Container.Bind<VisualPipeline>().AsSingle();
            
            Container.Bind<TurnPipelineRunner>().FromComponentInHierarchy();
            
            
        }
    }
}