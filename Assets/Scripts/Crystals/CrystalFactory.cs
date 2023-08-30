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
        private void Construct(DiContainer container, StaticDataProvider staticDataProvider,
            CrystalsTransform crystalsTransform)
        {
            _crystalsTransform = crystalsTransform;
            _container = container;
            _crystalPrefab = staticDataProvider.GetConfig<PrefabsConfig>().GetPrefab<Crystal>();
        }

        public Crystal SpawnCrystalAt(Transform spawnPoint)
        {
            var crystal = _container.InstantiatePrefabForComponent<Crystal>(_crystalPrefab);
            crystal.transform.SetParent(_crystalsTransform.transform);
            crystal.transform.position = spawnPoint.position;
            return crystal;
        }
    }
}