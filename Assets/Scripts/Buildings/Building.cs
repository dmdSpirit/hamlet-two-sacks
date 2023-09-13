#nullable enable

using System;
using HamletTwoSacks.Buildings.Config;
using HamletTwoSacks.Crystals.UI;
using HamletTwoSacks.Infrastructure.StaticData;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace HamletTwoSacks.Buildings
{
    public abstract class Building<TConfig, TTier> : MonoBehaviour
        where TConfig : BuildingConfig<TTier> where TTier : BuildingTier
    {
        private readonly Subject<Building<TConfig, TTier>> _onBuildingUpgraded = new();

        private IDisposable _sub = null!;
        private int _currentTierIndex;
        private TTier? _nextTier;

        [SerializeField]
        private SpriteRenderer _buildingImage = null!;

        [SerializeField]
        private CrystalCostPanel _costPanel = null!;

        protected TTier CurrentTier { get; private set; } = null!;
        protected TConfig Config { get; private set; } = null!;

        public IObservable<Building<TConfig, TTier>> OnBuildingUpgraded => _onBuildingUpgraded;
        public bool IsActive => CurrentTier?.IsActive ?? false;

        [Inject]
        private void GetConfig(StaticDataProvider staticDataProvider)
            => Config = staticDataProvider.GetConfig<TConfig>();

        protected void Start()
        {
            _sub = _costPanel.OnPricePayed.Subscribe(_ => Upgraded());
            ConfigureStartingTier();
            UpdateImage();
            ConfigureNextTier();
            OnStart();
            _onBuildingUpgraded.OnNext(this);
        }

        protected void OnDestroy()
        {
            _sub.Dispose();
            OnDestroyed();
        }

        protected abstract void OnStart();
        protected abstract void OnUpgraded();
        protected virtual void OnDestroyed() { }

        private void Upgraded()
        {
            Assert.IsNotNull(_nextTier);
            _currentTierIndex++;
            CurrentTier = _nextTier!;
            ConfigureNextTier();
            UpdateImage();
            OnUpgraded();
            _onBuildingUpgraded.OnNext(this);
        }

        private void ConfigureStartingTier()
        {
            if (Config.BuildingTiers == null)
            {
                Debug.LogError($"Could not configure building {name} cause there are no tier infos in config");
                return;
            }

            CurrentTier = Config.BuildingTiers[0];
        }

        private void ConfigureNextTier()
        {
            if (Config.BuildingTiers == null
                || _currentTierIndex + 1 >= Config.BuildingTiers.Count)
            {
                _nextTier = null;
                _costPanel.Disable();
                return;
            }

            _nextTier = Config.BuildingTiers[_currentTierIndex + 1];
            _costPanel.Enable();
            _costPanel.SetCost(_nextTier.Cost);
        }

        private void UpdateImage()
        {
            _buildingImage.sprite = CurrentTier.Image;
            _buildingImage.transform.localScale = new Vector3(CurrentTier.ImageSize.x, CurrentTier.ImageSize.y, 1);
            _buildingImage.transform.localPosition = CurrentTier.ImageOffset;
        }
    }
}