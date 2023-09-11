#nullable enable

using dmdspirit.Core.UI;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.Infrastructure.StaticData;
using HamletTwoSacks.Infrastructure.Time;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace HamletTwoSacks.Buildings
{
    public sealed class Mine : MonoBehaviour
    {
        private MineBuildingConfig _mineBuildingConfig = null!;

        private MineTierInfo? _nextTier;
        private int _currentTier;
        private bool _isBuilt;
        private RepeatingTimer _timer = null!;

        [SerializeField]
        private CrystalCostPanel _crystalCostPanel = null!;

        [SerializeField]
        private UpdatableProgressBar _progressBar = null!;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        [SerializeField]
        private GameObject _notBuiltImage = null!;

        [SerializeField]
        private GameObject _builtImage = null!;

        [Inject]
        private void Construct(StaticDataProvider staticDataProvider, TimeController timeController)
        {
            _mineBuildingConfig = staticDataProvider.GetConfig<MineBuildingConfig>();
            _timer = new RepeatingTimer(timeController);
        }

        private void Start()
        {
            _crystalCostPanel.OnPricePayed.Subscribe(OnUpgraded);
            _timer.OnFire.Subscribe(OnCrystalSpawn);
            _progressBar.StopShowing();
            ConfigureNextTier();
            UpdateImage();
        }

        private void OnDestroy()
        {
            _timer.Stop();
            _progressBar.StopShowing();
        }

        private void OnUpgraded(CrystalCostPanel _)
        {
            Assert.IsNotNull(_nextTier);
            _currentTier++;
            _isBuilt = true;
            StartSpawnTimer(_nextTier!.ProductionCooldown);
            ConfigureNextTier();
            UpdateImage();
        }

        private void StartSpawnTimer(float cooldown)
        {
            _timer.SetCooldown(cooldown);
            if (_timer.IsRunning)
                return;
            _progressBar.StartShowing(_timer.Progress);
            _timer.Start();
        }

        private void ConfigureNextTier()
        {
            if (!_isBuilt)
                _currentTier = -1;
            if (_mineBuildingConfig.TierInfos == null
                || _currentTier + 1 >= _mineBuildingConfig.TierInfos.Count)
            {
                _nextTier = null;
                _crystalCostPanel.Disable();
                return;
            }

            _nextTier = _mineBuildingConfig.TierInfos[_currentTier + 1];
            _crystalCostPanel.Enable();
            _crystalCostPanel.SetCost(_nextTier.Cost);
        }

        private void UpdateImage()
        {
            _notBuiltImage.SetActive(!_isBuilt);
            _builtImage.SetActive(_isBuilt);
        }

        private void OnCrystalSpawn(RepeatingTimer _)
            => _crystalSpawner.SpawnCrystal();
    }
}