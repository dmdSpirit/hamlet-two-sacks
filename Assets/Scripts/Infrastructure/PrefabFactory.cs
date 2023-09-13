#nullable enable

using System;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Infrastructure.StaticData;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    [UsedImplicitly]
    public sealed class PrefabFactory : IInitializable, IDisposable, IPrefabFactory
    {
        private readonly DiContainer _container;
        private readonly PrefabsConfig _prefabsConfig;
        private readonly CrystalsManager _crystalsManager;

        public PrefabFactory(DiContainer container, StaticDataProvider staticDataProvider,
            CrystalsManager crystalsManager)
        {
            _crystalsManager = crystalsManager;
            _container = container;
            _prefabsConfig = staticDataProvider.GetConfig<PrefabsConfig>();
        }

        public T CreateObject<T>() where T : MonoBehaviour
        {
            var prefab = _prefabsConfig.GetPrefab<T>();
            return _container.InstantiatePrefabForComponent<T>(prefab);
        }

        public void Initialize()
            => _crystalsManager.RegisterPrefabFactory(this);

        public void Dispose()
            => _crystalsManager.UnregisterPrefabFactory();
    }
}