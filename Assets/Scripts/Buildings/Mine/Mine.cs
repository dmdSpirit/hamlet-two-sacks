#nullable enable

using System;
using dmdspirit.Core.UI;
using HamletTwoSacks.Buildings.Mine.Config;
using HamletTwoSacks.Character;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Infrastructure.Time;
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
        private UpdatableProgressBar _progressBar = null!;

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
            UpdateTimer();
            _interactionSub = _buildingContinuesInteraction.IsButtonPressed.Subscribe(UpdateInteraction);
        }

        protected override void OnUpgraded()
            => UpdateTimer();

        protected override void OnDestroyed()
        {
            _timer.Stop();
            _progressBar.StopShowing();
            _interactionSub.Dispose();
        }

        private void UpdateInteraction(bool isPressed)
        {
            if (isPressed)
                _timer.SetWorker(nameof(Player), Config.PlayerWork);
            else
                _timer.SetWorker(nameof(Player), 0);
        }

        private void UpdateTimer()
        {
            if (!_progressBar.gameObject.activeInHierarchy
                && _timer.HadAnyProgress)
                _progressBar.StartShowing(_timer.Progress);

            if (!CurrentTier.IsActive)
            {
                _timer.SetWorker(nameof(Mine), 0f);
                return;
            }

            _timer.SetWorker(nameof(Mine), CurrentTier.Work);
        }

        private void OnCrystalSpawn(ProgressTimer _)
            => _crystalSpawner.SpawnCrystal();
    }
}