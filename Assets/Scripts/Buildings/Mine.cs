#nullable enable

using dmdspirit.Core.UI;
using HamletTwoSacks.Buildings.Configs;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Infrastructure.Time;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Buildings
{
    public sealed class Mine : Building<MineBuildingConfig, MineTier>
    {
        private RepeatingTimer _timer = null!;

        [SerializeField]
        private UpdatableProgressBar _progressBar = null!;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        [Inject]
        private void Construct(TimeController timeController)
            => _timer = new RepeatingTimer(timeController);

        protected override void OnStart()
        {
            _timer.OnFire.Subscribe(OnCrystalSpawn);
            UpdateTimer();
        }

        protected override void OnUpgraded()
            => UpdateTimer();

        protected override void OnDestroyed()
        {
            _timer.Stop();
            _progressBar.StopShowing();
        }

        private void UpdateTimer()
        {
            if (!CurrentTier.IsActive)
            {
                _timer.Stop();
                _progressBar.StopShowing();
                return;
            }

            _timer.SetCooldown(CurrentTier.ProductionCooldown);
            if (_timer.IsRunning)
                return;
            _timer.Start();
            _progressBar.StartShowing(_timer.Progress);
        }

        private void OnCrystalSpawn(RepeatingTimer _)
            => _crystalSpawner.SpawnCrystal();
    }
}