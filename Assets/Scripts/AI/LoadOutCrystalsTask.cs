#nullable enable

using System;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Time;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.AI
{
    public sealed class LoadOutCrystalsTask : Task
    {
        private IDisposable? _sub;
        private RepeatingTimer _repeatingTimer = null!;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        [SerializeField]
        private CrystalContainer _crystalContainer = null!;

        [SerializeField]
        private float _spawnCooldown = .1f;

        public override bool CanBeStarted => _crystalContainer.Crystals.Value > 0;
        public override bool CanBeSkipped => true;

        [Inject]
        private void Construct(TimeController timeController)
        {
            _repeatingTimer = new RepeatingTimer(timeController);
            _repeatingTimer.SetCooldown(_spawnCooldown);
            _sub = _repeatingTimer.OnFire.Subscribe(SpawnCrystal);
        }

        private void OnDestroy()
            => _sub?.Dispose();

        protected override void OnActivate()
            => _repeatingTimer.Start();

        protected override void OnDeactivate()
            => _repeatingTimer.Stop();

        protected override void OnComplete()
            => _repeatingTimer.Stop();

        public override void OnUpdate(float time) { }

        public override void OnFixedUpdate(float time) { }

        private void SpawnCrystal(RepeatingTimer _)
        {
            if (_crystalContainer.TryGetCrystal())
                _crystalSpawner.Spawn();
            else
                Complete();
        }
    }
}