#nullable enable
using System;
using HamletTwoSacks.Time;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace HamletTwoSacks.Level
{
    public sealed class Wiggle : MonoBehaviour
    {
        private TimeController _timeController = null!;

        private IDisposable _sub = null!;
        private float _originalY;
        private float _randomSeed;

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
        {
            _originalY = _target.localPosition.y;
            _randomSeed = Random.Range(0, 2 * Mathf.PI);
        }

        private void OnDestroy()
            => _sub.Dispose();

        private void OnFixedUpdate(float time)
        {
            Vector3 position = _target.localPosition;
            position.y = Mathf.Sin(_speed * (float)_timeController.TimePassed * Mathf.PI + _randomSeed) * _amplitude
                         + _originalY;
            _target.localPosition = position;
        }
    }
}