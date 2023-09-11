#nullable enable

using System;
using dmdspirit.Core.UI;
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

        [SerializeField]
        private ProgressBar _progressBar = null!;

        [Inject]
        private void Construct(TimeController timeController)
            => _timeController = timeController;

        private void Awake()
            => _progressBar.gameObject.SetActive(false);

        private void OnDestroy()
            => _sub?.Dispose();

        public void SetCooldown(float cooldown)
        {
            if (_cooldown != 0)
                _timePassed = cooldown * (_timePassed / _cooldown);
            _cooldown = cooldown;
            _progressBar.SetProgress(_timePassed / _cooldown);
        }

        public void Activate()
        {
            if (_isActive)
                return;
            _sub = _timeController.Update.Subscribe(OnUpdate);
            _isActive = true;
            _progressBar.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            if (!_isActive)
                return;
            _sub?.Dispose();
            _timePassed = 0;
            _isActive = false;
            _progressBar.gameObject.SetActive(false);
        }

        private void OnUpdate(float time)
        {
            _timePassed += time;
            if (_timePassed < _cooldown)
            {
                _progressBar.SetProgress(_timePassed / _cooldown);
                return;
            }

            _crystalSpawner.SpawnCrystal();
            _timePassed -= _cooldown;
            _progressBar.SetProgress(_timePassed / _cooldown);
        }
    }
}