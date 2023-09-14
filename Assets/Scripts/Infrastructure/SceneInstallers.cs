﻿#nullable enable

using System;
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

        private void BindFactory()
            => Container.Bind(typeof(ScenePrefabFactory), typeof(IInitializable), typeof(IDisposable))
                .To<ScenePrefabFactory>().AsSingle().NonLazy();
    }
}