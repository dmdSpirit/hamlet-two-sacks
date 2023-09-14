#nullable enable
using System;
using HamletTwoSacks.Time;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Level
{
    public sealed class Wiggle : MonoBehaviour
    {
        private TimeController _timeController = null!;

        private IDisposable _sub = null!;
        private float _originalY;

        [SerializeField]
        private Transform _target = null!;

        [SerializeField]
        private float _speed = 1f;

        [SerializeField]
        private float _amplitude = 1f;

        [Inject]
        private void Construct(TimeController timeController)
        {
            _timeController = timeController;
            _sub = timeController.FixedUpdate.Subscribe(OnFixedUpdate);
        }

        private void Awake()
            => _originalY = _target.position.y;

        private void OnFixedUpdate(float time)
        {
            Vector3 position = _target.position;
            position.y = Mathf.Sin(_speed * (float)_timeController.TimePassed * Mathf.PI) * _amplitude + _originalY;
            _target.position = position;
        }
    }
}