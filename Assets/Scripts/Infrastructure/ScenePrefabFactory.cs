#nullable enable

using System;
using HamletTwoSacks.Infrastructure.StaticData;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    [UsedImplicitly]
    public sealed class ScenePrefabFactory : IPrefabFactory, IDisposable
    {
        private readonly DiContainer _container;
        private readonly PrefabsConfig _prefabsConfig;
        private readonly IGameFactory _gameFactory;

        public ScenePrefabFactory(DiContainer container, StaticDataProvider staticDataProvider,
            IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _container = container;
            _prefabsConfig = staticDataProvider.GetConfig<PrefabsConfig>();
            gameFactory.BindSceneFactory(this);
        }

        public T CreateObject<T>() where T : MonoBehaviour
        {
            var prefab = _prefabsConfig.GetPrefab<T>();
            return _container.InstantiatePrefabForComponent<T>(prefab);
        }

        public void Dispose()
            => _gameFactory.UnbindSceneFactory(this);
    }
}