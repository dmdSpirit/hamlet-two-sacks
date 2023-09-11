#nullable enable

using System;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Buildings
{
    public sealed class TimedCrystalSpawner : MonoBehaviour
    {
        private TimeController _timeController = null!;

        private bool _isActive;
        private IDisposable? _sub;
        private float _cooldown;
        private float _timePassed;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        [Inject]
        private void Construct(TimeController timeController)
            => _timeController = timeController;

        private void OnDestroy()
            => _sub?.Dispose();

        public void SetCooldown(float cooldown)
        {
            _cooldown = cooldown;
            _timePassed = 0;
        }

        public void Activate()
        {
            if (_isActive)
                return;
            _sub = _timeController.Update.Subscribe(OnUpdate);
            _isActive = true;
        }

        public void Deactivate()
        {
            if (!_isActive)
                return;
            _sub?.Dispose();
            _timePassed = 0;
            _isActive = false;
        }

        private void OnUpdate(float time)
        {
            _timePassed += time;
            if (_timePassed < _cooldown)
                return;
            _crystalSpawner.SpawnCrystal();
            _timePassed -= _cooldown;
        }
    }
}