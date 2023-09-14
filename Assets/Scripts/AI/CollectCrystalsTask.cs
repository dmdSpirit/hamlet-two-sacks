#nullable enable

using System;
using HamletTwoSacks.Crystals;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.AI
{
    public sealed class CollectCrystalsTask : Task
    {
        private IDisposable? _sub;

        [SerializeField]
        private CrystalCollector _crystalCollector = null!;

        [SerializeField]
        private CrystalContainer _crystalContainer = null!;

        public override bool CanBeStarted => _crystalContainer.IsFull;
        public override bool CanBeSkipped => true;

        private void Start()
        {
            _crystalCollector.SetCollectionCheck(CanCollectCrystal);
            _sub = _crystalCollector.OnCrystalCollected.Subscribe(OnCrystalCollected);
            _crystalCollector.Deactivate();
        }

        private void OnDestroy()
            => _sub?.Dispose();

        protected override void OnActivate()
            => _crystalCollector.Activate();

        protected override void OnDeactivate()
            => _crystalCollector.Deactivate();

        protected override void OnComplete() { }

        public override void OnUpdate(float time) { }

        public override void OnFixedUpdate(float time) { }

        private bool CanCollectCrystal()
            => _crystalContainer.Crystals.Value + _crystalCollector.ActiveCommands < _crystalContainer.Capacity.Value;

        private void OnCrystalCollected(Unit _)
        {
            _crystalContainer.AddCrystal();
            if (_crystalContainer.IsFull)
                Complete();
        }
    }
}