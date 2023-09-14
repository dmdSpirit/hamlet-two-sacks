#nullable enable

using System;
using HamletTwoSacks.AI;
using HamletTwoSacks.Buildings.Crate.Config;
using HamletTwoSacks.Crystals;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Crate
{
    public sealed class Crate : Building<CrateConfig, CrateTier>
    {
        private IDisposable _collectorSub = null!;

        [SerializeField]
        private CrystalContainer _crystalContainer = null!;

        [SerializeField]
        private CrystalCollector _crystalCollector = null!;

        [SerializeField]
        private BuildingTimedInteraction _buildingTimedInteraction = null!;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        protected override void OnStart()
        {
            _collectorSub = _crystalCollector.OnCrystalCollected.Subscribe(OnCrystalCollected);
            _crystalCollector.SetCollectionCheck(CanCollectCrystal);
            _buildingTimedInteraction.OnActionFire.Subscribe(DropCrystal);
            _crystalContainer.SetCapacity(CurrentTier.Capacity);
            _crystalContainer.Crystals.Subscribe(_ => UpdateInteraction());
            UpdateInteraction();
        }

        protected override void OnUpgraded()
            => UpdateInteraction();

        protected override void OnDestroyed()
            => _collectorSub.Dispose();

        private void OnCrystalCollected(Unit _)
            => _crystalContainer.AddCrystal();

        private bool CanCollectCrystal()
            => _crystalContainer.Crystals.Value + _crystalCollector.ActiveCommands < CurrentTier.Capacity;

        private void DropCrystal(Unit _)
        {
            if (_crystalContainer.TryGetCrystal())
                _crystalSpawner.Spawn();
        }

        private void UpdateInteraction()
        {
            UpdateCollectorState();
            UpdateInteractionState();
        }

        private void UpdateCollectorState()
        {
            if (_crystalContainer.IsFull)
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
            if (_crystalContainer.Crystals.Value <= 0)
            {
                _buildingTimedInteraction.Deactivate();
                return;
            }

            if (CurrentTier.IsActive == _buildingTimedInteraction.IsActive)
                return;
            if (CurrentTier.IsActive)
                _buildingTimedInteraction.Activate();
            else
                _buildingTimedInteraction.Deactivate();
        }
    }
}