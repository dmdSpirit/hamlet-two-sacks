#nullable enable

using dmdspirit.Core;
using HamletTwoSacks.Character;
using HamletTwoSacks.Infrastructure.LifeCycle;
using HamletTwoSacks.Infrastructure.LifeCycle.States;
using HamletTwoSacks.UI;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class GameInstallers : MonoInstaller
    {
        [SerializeField]
        private CharacterFactory _characterFactory = null!;

        [SerializeField]
        private CameraController _cameraController = null!;

        [SerializeField]
        private CameraTargetFollow _cameraTargetFollow = null!;

        public override void InstallBindings()
        {
            BindCharacters();
            BindLifeCycle();
            BindCamera();
        }

        private void BindCharacters()
        {
            Container.Bind<IPlayerFactory>().FromInstance(_characterFactory);
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
            Container.Bind<CameraTargetFollow>().FromInstance(_cameraTargetFollow);
        }
    }
}