#nullable enable

using HamletTwoSacks.Infrastructure.LifeCycle;
using HamletTwoSacks.Infrastructure.LifeCycle.States;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class GameInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLifeCycle();
        }

        private void BindLifeCycle()
        {
            Container.BindInterfacesTo<GameLifeCycle>().AsSingle();
            
            Container.Bind<InitializeGameState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<NewGameState>().AsSingle();
            Container.Bind<ExitGameState>().AsSingle();
        }
    }
}