#nullable enable

using System;
using DG.Tweening;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Commands
{
    [UsedImplicitly]
    public sealed class FlyObjectToCommand : ICommand
    {
        private readonly Subject<ICommand> _onCompleted = new();

        private readonly Transform _destination;
        private readonly float _speed;
        private readonly float _completionRadius;

        private Tweener? _tweener;

        public IObservable<ICommand> OnCompleted => _onCompleted;
        public Transform Target { get; }

        public FlyObjectToCommand(Transform target, Transform destination, float speed, float completionRadius)
        {
            _completionRadius = completionRadius;
            _speed = speed;
            _destination = destination;
            Target = target;
        }

        // FIXME (Stas): DOTween uses unity update system. I should figure a way to bypass this.
        // - Stas 08 September 2023
        public void Start()
        {
            Debug.Log($"fly started");
            _tweener = Target.DOMove(_destination.position, _speed).SetSpeedBased(true);
            _tweener.OnUpdate(UpdateTarget);
            _tweener.onComplete += Complete;
        }

        public void Interrupt()
        {
            _tweener?.Kill();
            _tweener = null;
        }

        // TODO (Stas): Should I track that crystal is still alive?
        // - Stas 08 September 2023
        private void UpdateTarget()
        {
            if (Vector3.Distance(Target.position, _destination.position) <= _completionRadius)
                return;
            _tweener!.ChangeEndValue(_destination.position, true);
        }

        private void Complete()
        {
            _tweener.Kill();
            _tweener = null;
            _onCompleted.OnNext(this);
        }
    }
}