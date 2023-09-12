#nullable enable

using System;
using JetBrains.Annotations;
using UniRx;
using Zenject;

namespace HamletTwoSacks.Time
{
    [UsedImplicitly]
    public sealed class TimeController : ITickable, IFixedTickable, ILateTickable
    {
        private readonly ReactiveProperty<bool> _isTimeRunning = new();
        private readonly Subject<float> _update = new();
        private readonly Subject<float> _fixedUpdate = new();
        private readonly Subject<float> _lateUpdate = new();

        public IReadOnlyReactiveProperty<bool> IsTimeRunning => _isTimeRunning;
        public IObservable<float> Update => _update;
        public IObservable<float> FixedUpdate => _fixedUpdate;
        public IObservable<float> LateUpdate => _lateUpdate;

        public float DeltaTime => UnityEngine.Time.deltaTime;
        public float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime;

        public void StartTime()
            => _isTimeRunning.Value = true;

        public void StopTime()
            => _isTimeRunning.Value = false;

        public void Tick()
        {
            if (!_isTimeRunning.Value)
                return;
            _update.OnNext(DeltaTime);
        }

        public void FixedTick()
        {
            if (!_isTimeRunning.Value)
                return;
            _fixedUpdate.OnNext(FixedDeltaTime);
        }

        public void LateTick()
        {
            if (!_isTimeRunning.Value)
                return;
            _lateUpdate.OnNext(DeltaTime);
        }
    }
}