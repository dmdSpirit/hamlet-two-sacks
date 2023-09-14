#nullable enable

using System;
using HamletTwoSacks.Infrastructure;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.AI
{
    public abstract class Task : MonoBehaviour
    {
        private readonly Subject<Task> _completed = new();

        public virtual bool CanBeStarted => true;
        public virtual bool CanBeSkipped => false;

        public bool IsActive { get; private set; }
        public IObservable<Task> Completed => _completed;

        public void Activate()
        {
            OnActivate();
            IsActive = true;
        }

        public void Deactivate()
        {
            OnDeactivate();
            IsActive = false;
        }

        public void Complete()
        {
            OnComplete();
            _completed.OnNext(this);
            Deactivate();
        }

        public abstract void Initialize(SystemReferences references);
        public abstract void OnUpdate(float time);
        public abstract void OnFixedUpdate(float time);
        protected abstract void OnActivate();
        protected abstract void OnDeactivate();
        protected abstract void OnComplete();
    }
}