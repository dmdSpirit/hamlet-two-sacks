#nullable enable

using dmdspirit.Core;
using HamletTwoSacks.Character;
using HamletTwoSacks.Commands;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Infrastructure.LifeCycle;
using HamletTwoSacks.Infrastructure.LifeCycle.States;
using HamletTwoSacks.Infrastructure.StaticData;
using HamletTwoSacks.Infrastructure.Time;
using HamletTwoSacks.Input;
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

            Container.Bind<StaticDataProvider>().AsSingle().NonLazy();
        }

        private void BindCharacters()
        {
            Container.Bind<Player>().AsSingle().NonLazy();
            Container.Bind<CharactersManager>().AsSingle();
        }

        private void BindLifeCycle()
        {
            Container.Bind<IGameLifeCycle>().To<GameLifeCycle>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenShower>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<LevelManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeController>().AsSingle();

            Container.Bind<InitializeGameState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<NewGameState>().AsSingle();
            Container.Bind<ExitGameState>().AsSingle();
            Container.Bind<GameState>().AsSingle();
        }

        private void BindCamera()
        {
            Container.Bind<CameraController>().FromInstance(_cameraController);
        }

        private void BindCommands()
        {
            Container.Bind<CommandsFactory>().AsSingle();

            Container.Bind<FlyObjectToCommand>().AsTransient();
        }

        private void BindInput()
        {
            Container.Bind<ActionButtonReader>().AsSingle().NonLazy();
        }
    }
}