#nullable enable

using System;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Time
{
    public class RepeatingTimer
    {
        private readonly TimeController _timeController;

        private readonly Subject<RepeatingTimer> _onFire = new();
        private readonly ReactiveProperty<float> _progress = new();

        private float _cooldown;
        private float _timePassed;
        private IDisposable? _sub;

        public bool IsRunning { get; private set; }
        public IObservable<RepeatingTimer> OnFire => _onFire;
        public IReadOnlyReactiveProperty<float> Progress => _progress;

        public RepeatingTimer(TimeController timeController)
            => _timeController = timeController;

        public void Start()
        {
            _timePassed = 0;
            if (IsRunning)
                return;
            _sub = _timeController.Update.Subscribe(OnUpdate);
            IsRunning = true;
        }

        public void SetCooldown(float cooldown)
        {
            if (_cooldown != 0)
                _timePassed = cooldown * (_timePassed / _cooldown);
            _cooldown = cooldown;
        }

        public void Stop()
        {
            _timePassed = 0;
            _sub?.Dispose();
            IsRunning = false;
        }

        private void OnUpdate(float time)
        {
            if (_cooldown == 0)
            {
                _onFire.OnNext(this);
                return;
            }

            _timePassed += time;
            if (_timePassed < _cooldown)
            {
                _progress.Value = _cooldown == 0 ? 0 : Mathf.Clamp(_timePassed / _cooldown, 0, 1);
                return;
            }

            _onFire.OnNext(this);
            _timePassed -= _cooldown;
            _progress.Value = _cooldown == 0 ? 0 : Mathf.Clamp(_timePassed / _cooldown, 0, 1);
        }
    }
}