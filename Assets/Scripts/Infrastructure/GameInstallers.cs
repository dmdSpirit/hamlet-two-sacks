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

        public override void InstallBindings()
        {
            BindCharacters();
            BindLifeCycle();
        }

        private void BindCharacters()
        {
            Container.Bind<IPlayerFactory>().FromInstance(_characterFactory);
            Container.Bind<ICharactersManager>().To<CharactersManager>().AsSingle();
        }

        private void BindLifeCycle()
        {
            Container.Bind<IGameLifeCycle>().To<GameLifeCycle>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenShower>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle();

            Container.Bind<InitializeGameState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<NewGameState>().AsSingle();
            Container.Bind<ExitGameState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
        }
    }
}