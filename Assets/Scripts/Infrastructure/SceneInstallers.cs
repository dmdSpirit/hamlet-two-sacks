#nullable enable

using HamletTwoSacks.Character;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class SceneInstallers : MonoInstaller
    {
        [SerializeField]
        private PlayerSpawner _playerSpawner = null!;

        
        public override void InstallBindings()
        {
            BindCharacters();
        }

        private void BindCharacters()
        {
            // Container.Bind<IPlayerSpawner>().FromInstance(_playerSpawner);
        }
    }
}