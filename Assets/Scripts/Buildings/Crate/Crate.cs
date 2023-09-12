#nullable enable

using System;
using HamletTwoSacks.Buildings.Crate.Config;
using HamletTwoSacks.Crystals;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Crate
{
    public sealed class Crate : Building<CrateBuildingConfig, CrateTier>
    {
        private readonly ReactiveProperty<int> _crystals = new();

        private IDisposable _collectorSub = null!;

        [SerializeField]
        private CrystalCollector _crystalCollector = null!;

        public IReadOnlyReactiveProperty<int> Crystals => _crystals;
        public int Capacity => CurrentTier.Capacity;

        protected override void OnStart()
        {
            _collectorSub = _crystalCollector.OnCrystalCollected.Subscribe(OnCrystalCollected);
            if (CurrentTier.IsActive)
                _crystalCollector.Activate();
            else
                _crystalCollector.Deactivate();
        }

        protected override void OnUpgraded()
        {
            if (_crystals.Value < CurrentTier.Capacity)
                _crystalCollector.Activate();
        }

        protected override void OnDestroyed()
            => _collectorSub.Dispose();

        private void OnCrystalCollected(Unit _)
        {
            _crystals.Value++;
            if (_crystals.Value >= CurrentTier.Capacity)
                _crystalCollector.Deactivate();
        }
    }
}