#nullable enable

using HamletTwoSacks.Characters.PlayerControl;
using HamletTwoSacks.Level;
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

        [SerializeField]
        private LevelTransforms _levelTransforms = null!;

        public override void InstallBindings()
        {
            BindCharacters();
            BindCamera();
            BindLevel();
            BindFactory();
        }

        private void BindCamera()
        {
            Container.Bind<VCTest>().FromInstance(_vcTest);
        }

        private void BindCharacters()
        {
            Container.Bind<IPlayerSpawner>().FromInstance(_playerSpawner);
        }

        private void BindLevel()
        {
            Container.Bind<LevelTransforms>().FromInstance(_levelTransforms);
        }

        // HACK (Stas): It seems that I should bind PrefabFactory in every scene context for it to get dependencies from said context also.
        // - Stas 13 September 2023
        private void BindFactory()
            => Container.BindInterfacesTo<PrefabFactory>().AsSingle();
    }
}