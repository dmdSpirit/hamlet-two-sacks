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

        [SerializeField]
        private BuildingInteraction _buildingInteraction = null!;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        public IReadOnlyReactiveProperty<int> Crystals => _crystals;
        public int Capacity => CurrentTier.Capacity;

        protected override void OnStart()
        {
            _collectorSub = _crystalCollector.OnCrystalCollected.Subscribe(OnCrystalCollected);
            _crystalCollector.SetCollectionCheck(CanCollectCrystal);
            _buildingInteraction.OnActionFire.Subscribe(DropCrystal);
            _crystals.Subscribe(_ => UpdateInteraction());
        }

        protected override void OnUpgraded()
            => UpdateInteraction();

        protected override void OnDestroyed()
            => _collectorSub.Dispose();

        private void OnCrystalCollected(Unit _)
            => _crystals.Value++;

        private bool CanCollectCrystal()
            => _crystals.Value + _crystalCollector.ActiveCommands < CurrentTier.Capacity;

        private void DropCrystal(Unit _)
        {
            if (_crystals.Value <= 0)
                return;
            _crystalSpawner.SpawnCrystal();
            _crystals.Value--;
        }

        private void UpdateInteraction()
        {
            UpdateCollectorState();
            UpdateInteractionState();
        }

        private void UpdateCollectorState()
        {
            if (_crystals.Value >= CurrentTier.Capacity)
            {
                _crystalCollector.Deactivate();
                return;
            }

            if (CurrentTier.IsActive == _crystalCollector.IsActive)
                return;
            if (CurrentTier.IsActive)
                _crystalCollector.Activate();
            else
                _crystalCollector.Deactivate();
        }

        private void UpdateInteractionState()
        {
            if (_crystals.Value <= 0)
            {
                _buildingInteraction.Deactivate();
                return;
            }

            if (CurrentTier.IsActive == _buildingInteraction.IsActive)
                return;
            if (CurrentTier.IsActive)
                _buildingInteraction.Activate();
            else
                _buildingInteraction.Deactivate();
        }
    }
}