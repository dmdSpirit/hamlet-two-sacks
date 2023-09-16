#nullable enable

using HamletTwoSacks.Characters;
using HamletTwoSacks.Characters.PlayerControl;
using HamletTwoSacks.Commands;
using HamletTwoSacks.Infrastructure.LifeCycle;
using HamletTwoSacks.Infrastructure.LifeCycle.States;
using HamletTwoSacks.Infrastructure.StaticData;
using HamletTwoSacks.Input;
using HamletTwoSacks.Time;
using HamletTwoSacks.UI;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class GameInstallers : MonoInstaller
    {
        [SerializeField]
        private CameraController _cameraController = null!;

        public override void InstallBindings()
        {
            BindCharacters();
            BindLifeCycle();
            BindCamera();
            BindCommands();
            BindInput();
            BindManagers();
            BindFactory();

            Container.Bind<StaticDataProvider>().AsSingle().NonLazy();
        }

        private void BindCharacters()
        {
            Container.Bind<Player>().AsSingle().NonLazy();
            Container.Bind<PlayerManager>().AsSingle();
        }

        private void BindManagers()
        {
            Container.Bind<EntityManager>().AsSingle();
        }

        private void BindLifeCycle()
        {
            Container.BindInterfacesTo<GameLifeCycle>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenShower>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<LevelManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeController>().AsSingle();

            Container.Bind<InitializeGameState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<NewGameState>().AsSingle();
            Container.Bind<ExitGameState>().AsSingle();
            Container.Bind<GameState>().AsSingle();
            Container.Bind<SelectLevelState>().AsSingle();
        }

        private void BindCamera()
            => Container.Bind<CameraController>().FromInstance(_cameraController);

        private void BindFactory()
            => Container.BindInterfacesTo<GameFactory>().AsSingle();

        private void BindCommands()
        {
            Container.Bind<CommandsFactory>().AsSingle();

            Container.Bind<FlyObjectToCommand>().AsTransient();
        }

        private void BindInput()
            => Container.BindInterfacesTo<ActionButtonsReader>().AsSingle().NonLazy();
    }
}