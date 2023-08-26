#nullable enable

using HamletTwoSacks.Character;
using HamletTwoSacks.Infrastructure.LifeCycle;
using HamletTwoSacks.Infrastructure.LifeCycle.States;
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
            Container.Bind<CharacterFactory>().FromInstance(_characterFactory);
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