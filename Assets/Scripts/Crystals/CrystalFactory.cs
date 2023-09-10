#nullable enable

using HamletTwoSacks.Infrastructure.StaticData;
using HamletTwoSacks.Level;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    [UsedImplicitly]
    public sealed class CrystalFactory : ICrystalFactory
    {
        private DiContainer _container = null!;
        private LevelTransforms _levelTransforms = null!;

        private Crystal _crystalPrefab = null!;

        [Inject]
        private void Construct(DiContainer container, StaticDataProvider staticDataProvider,
            LevelTransforms levelTransforms)
        {
            _levelTransforms = levelTransforms;
            _container = container;
            _crystalPrefab = staticDataProvider.GetConfig<PrefabsConfig>().GetPrefab<Crystal>();
        }

        public Crystal SpawnCrystal()
        {
            var crystal = _container.InstantiatePrefabForComponent<Crystal>(_crystalPrefab);
            crystal.transform.SetParent(_levelTransforms.Crystals);
            return crystal;
        }

        public void DestroyCrystal(Crystal crystal)
            => Object.Destroy(crystal.gameObject);
    }
}