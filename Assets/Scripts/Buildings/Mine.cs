#nullable enable

using HamletTwoSacks.Crystals;
using HamletTwoSacks.Infrastructure.StaticData;
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

        [SerializeField]
        private CrystalCostPanel _crystalCostPanel = null!;

        [SerializeField]
        private TimedCrystalSpawner _timedCrystalSpawner = null!;

        [SerializeField]
        private GameObject _notBuiltImage = null!;

        [SerializeField]
        private GameObject _builtImage = null!;

        [Inject]
        private void Construct(StaticDataProvider staticDataProvider)
            => _mineBuildingConfig = staticDataProvider.GetConfig<MineBuildingConfig>();

        private void Start()
        {
            _crystalCostPanel.OnPricePayed.Subscribe(OnUpgraded);
            ConfigureNextTier();
            UpdateImage();
        }

        private void OnUpgraded(CrystalCostPanel _)
        {
            Assert.IsNotNull(_nextTier);
            _currentTier++;
            _isBuilt = true;
            _timedCrystalSpawner.SetCooldown(_nextTier!.ProductionCooldown);
            _timedCrystalSpawner.Activate();
            ConfigureNextTier();
            UpdateImage();
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
    }
}