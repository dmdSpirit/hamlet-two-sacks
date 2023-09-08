#nullable enable

using HamletTwoSacks.Infrastructure.StaticData;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    [UsedImplicitly]
    public sealed class CrystalFactory : ICrystalFactory
    {
        private DiContainer _container = null!;
        private CrystalsTransform _crystalsTransform = null!;

        private Crystal _crystalPrefab = null!;

        [Inject]
        private void Construct(DiContainer container, StaticDataProvider staticDataProvider)
        {
            _container = container;
            _crystalPrefab = staticDataProvider.GetConfig<PrefabsConfig>().GetPrefab<Crystal>();
        }

        public Crystal SpawnCrystal()
        {
            var crystal = _container.InstantiatePrefabForComponent<Crystal>(_crystalPrefab);
            return crystal;
        }

        public void DestroyCrystal(Crystal crystal)
            => Object.Destroy(crystal.gameObject);
    }
}