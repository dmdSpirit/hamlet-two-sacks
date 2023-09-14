#nullable enable

using System;
using HamletTwoSacks.Buildings.Mine.Config;
using HamletTwoSacks.Characters.PlayerControl;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Time;
using HamletTwoSacks.Time.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Buildings.Mine
{
    public sealed class Mine : Building<MineBuildingConfig, MineTier>
    {
        private ProgressTimer _timer = null!;
        private IDisposable _interactionSub = null!;

        [SerializeField]
        private TimerProgressBar _progressBar = null!;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        [SerializeField]
        private BuildingContinuesInteraction _buildingContinuesInteraction = null!;

        [Inject]
        private void Construct(TimeController timeController)
            => _timer = new ProgressTimer(timeController);

        protected override void OnStart()
        {
            _timer.OnFire.Subscribe(OnCrystalSpawn);
            _timer.SetGoal(Config.CrystalProductionTime);
            _timer.Start();
            _progressBar.SetTimer(_timer);
            UpdateTimer();
            _interactionSub = _buildingContinuesInteraction.IsButtonPressed.Subscribe(UpdatePlayerInteraction);
            UpdateInteractionState();
        }

        protected override void OnUpgraded()
        {
            UpdateTimer();
            UpdateInteractionState();
        }

        protected override void OnDestroyed()
        {
            _timer.Stop();
            _interactionSub.Dispose();
        }

        private void UpdatePlayerInteraction(bool isPressed)
        {
            if (isPressed)
                _timer.SetWorker(nameof(Player), Config.PlayerWork);
            else
                _timer.SetWorker(nameof(Player), 0);
        }

        private void UpdateTimer()
        {
            if (!CurrentTier.IsActive)
            {
                _timer.SetWorker(nameof(Mine), 0f);
                return;
            }

            _timer.SetWorker(nameof(Mine), CurrentTier.Work);
        }

        private void UpdateInteractionState()
        {
            if (CurrentTier.IsActive == _buildingContinuesInteraction.IsActive)
                return;
            if (CurrentTier.IsActive)
                _buildingContinuesInteraction.Activate();
            else
                _buildingContinuesInteraction.Deactivate();
        }

        private void OnCrystalSpawn(ProgressTimer _)
            => _crystalSpawner.Spawn();
    }
}