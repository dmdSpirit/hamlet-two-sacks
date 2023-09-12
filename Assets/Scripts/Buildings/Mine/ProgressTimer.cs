#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using HamletTwoSacks.Infrastructure.Time;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Mine
{
    public class ProgressTimer
    {
        private readonly TimeController _timeController;

        private readonly Subject<ProgressTimer> _onFire = new();
        private readonly ReactiveProperty<float> _progress = new();
        private readonly Dictionary<string, float> _workers = new();

        private float _goal;
        private float _accumulatedProgress;
        private IDisposable? _sub;

        public bool HadAnyProgress { get; private set; }
        public bool IsRunning { get; private set; }
        public IObservable<ProgressTimer> OnFire => _onFire;
        public IReadOnlyReactiveProperty<float> Progress => _progress;

        public ProgressTimer(TimeController timeController)
            => _timeController = timeController;

        public void Start()
        {
            ResetTimer();
            if (IsRunning)
                return;
            _sub = _timeController.Update.Subscribe(OnUpdate);
            IsRunning = true;
        }

        public void SetGoal(float goal)
        {
            if (goal != 0)
                _accumulatedProgress = goal * (_accumulatedProgress / goal);
            _goal = goal;
        }

        public void SetWorker(string workerID, float work)
        {
            if (!_workers.ContainsKey(workerID))
            {
                _workers.Add(workerID, work);
                return;
            }

            _workers[workerID] = work;
        }

        public void Stop()
        {
            ResetTimer();
            _sub?.Dispose();
            IsRunning = false;
        }

        private void OnUpdate(float time)
        {
            float progress = time * _workers.Sum(vk => vk.Value);
            if (!HadAnyProgress
                && progress > 0)
                HadAnyProgress = true;
            _accumulatedProgress += progress;
            if (_accumulatedProgress < _goal)
            {
                _progress.Value = _goal == 0 ? 0 : Mathf.Clamp(_accumulatedProgress / _goal, 0, 1);
                return;
            }

            _onFire.OnNext(this);
            _accumulatedProgress -= _goal;
            _progress.Value = _goal == 0 ? 0 : Mathf.Clamp(_accumulatedProgress / _goal, 0, 1);
        }

        private void ResetTimer()
        {
            HadAnyProgress = false;
            _accumulatedProgress = 0f;
            _workers.Clear();
        }
    }
}