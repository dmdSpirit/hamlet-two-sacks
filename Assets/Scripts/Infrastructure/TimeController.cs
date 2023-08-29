#nullable enable
using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    [UsedImplicitly]
    public sealed class TimeController : ITickable, IFixedTickable, ILateTickable
    {
        private readonly ReactiveProperty<bool> _isTimeRunning = new();
        private readonly Subject<Unit> _update = new();
        private readonly Subject<Unit> _fixedUpdate = new();
        private readonly Subject<Unit> _lateUpdate = new();

        public IReadOnlyReactiveProperty<bool> IsTimeRunning => _isTimeRunning;
        public IObservable<Unit> Update => _update;
        public IObservable<Unit> FixedUpdate => _fixedUpdate;
        public IObservable<Unit> LateUpdate => _lateUpdate;

        public float DeltaTime => Time.deltaTime;
        public float FixedDeltaTime => Time.fixedDeltaTime;

        public void StartTime()
            => _isTimeRunning.Value = true;

        public void StopTime()
            => _isTimeRunning.Value = false;

        public void Tick()
        {
            if (!_isTimeRunning.Value)
                return;
            _update.OnNext(Unit.Default);
        }

        public void FixedTick()
        {
            if (!_isTimeRunning.Value)
                return;
            _fixedUpdate.OnNext(Unit.Default);
        }

        public void LateTick()
        {
            if (!_isTimeRunning.Value)
                return;
            _lateUpdate.OnNext(Unit.Default);
        }
    }
}