#nullable enable

using System;
using HamletTwoSacks.Infrastructure;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Commands
{
    [UsedImplicitly]
    public sealed class FlyObjectToCommand : ICommand
    {
        private readonly Subject<ICommand> _onCompleted = new();

        private readonly TimeController _timeController;

        private readonly Transform _destination;
        private readonly float _speed;
        private readonly float _completionRadius;

        private IDisposable? _sub;

        public IObservable<ICommand> OnCompleted => _onCompleted;
        public Transform Target { get; }

        public FlyObjectToCommand(Transform target, Transform destination, float speed, float completionRadius,
            TimeController timeController)
        {
            _timeController = timeController;
            _completionRadius = completionRadius;
            _speed = speed;
            _destination = destination;
            Target = target;
        }

        public void Start()
            => _sub = _timeController.FixedUpdate.Subscribe(OnUpdate);

        public void Interrupt()
            => _sub?.Dispose();
        
        private void OnUpdate(float time)
        {
            float distance = Vector3.Distance(Target.position, _destination.position);
            if (distance <= _completionRadius)
            {
                _onCompleted.OnNext(this);
                _sub?.Dispose();
                return;
            }

            Target.position = Vector3.MoveTowards(Target.position, _destination.position, time * _speed);
        }
    }
}