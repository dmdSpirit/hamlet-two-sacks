#nullable enable

using System;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Infrastructure;
using UniRx;

namespace HamletTwoSacks.AI.SimpleBrain.Tasks
{
    public sealed class CollectCrystalsTask : Task
    {
        private IDisposable? _sub;

        private CrystalCollector _crystalCollector = null!;
        private CrystalContainer _crystalContainer = null!;

        public override bool CanBeStarted => _crystalContainer.IsFull;
        public override bool CanBeSkipped => true;

        private void OnDestroy()
            => _sub?.Dispose();

        public override void Initialize(SystemReferences references)
        {
            _crystalCollector = references.GetSystemWithCheck<CrystalCollector>();
            _crystalContainer = references.GetSystemWithCheck<CrystalContainer>();
            _crystalCollector.SetCollectionCheck(CanCollectCrystal);
            _sub = _crystalCollector.OnCrystalCollected.Subscribe(OnCrystalCollected);
            _crystalCollector.Deactivate();
        }

        public override void OnUpdate(float time) { }

        public override void OnFixedUpdate(float time) { }
        
        protected override void OnActivate()
            => _crystalCollector.Activate();

        protected override void OnDeactivate()
            => _crystalCollector.Deactivate();

        protected override void OnComplete() { }

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