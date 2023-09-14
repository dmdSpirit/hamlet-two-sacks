#nullable enable

using System;
using HamletTwoSacks.Level;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class SceneInstallers : MonoInstaller
    {
        [SerializeField]
        private VCTest _vcTest = null!;

        [SerializeField]
        private LevelTransforms _levelTransforms = null!;

        public override void InstallBindings()
        {
            BindCamera();
            BindLevel();
            BindFactory();
        }

        private void BindCamera()
        {
            Container.Bind<VCTest>().FromInstance(_vcTest);
        }

        private void BindLevel()
        {
            Container.Bind<LevelTransforms>().FromInstance(_levelTransforms);
        }

        private void BindFactory()
            => Container.Bind(typeof(ScenePrefabFactory), typeof(IDisposable)).To<ScenePrefabFactory>().AsSingle()
                .NonLazy();
    }
}