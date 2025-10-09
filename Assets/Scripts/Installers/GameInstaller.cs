using Zenject;
using Services;
using State;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BoardState>().AsSingle();

            Container.BindInterfacesAndSelfTo<TurnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CombatService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AbilityService>().AsSingle();

            Container.Bind<RandomService>().AsSingle();
        }
    }
}