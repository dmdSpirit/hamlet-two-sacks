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

        [SerializeField]
        private VCTest _vcTest = null!;
        
        public override void InstallBindings()
        {
            BindCharacters();
            BindCamera();
        }

        private void BindCamera()
        {
            Container.Bind<VCTest>().FromInstance(_vcTest);
        }

        private void BindCharacters()
        {
            Container.Bind<IPlayerSpawner>().FromInstance(_playerSpawner);
        }
    }
}