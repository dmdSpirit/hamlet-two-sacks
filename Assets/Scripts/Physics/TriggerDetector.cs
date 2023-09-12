#nullable enable

using System;
using dmdspirit.Core.CommonInterfaces;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Physics
{
    public sealed class TriggerDetector : MonoBehaviour, IActivatable
    {
        private readonly Subject<Collider2D> _onTriggerEnter = new();
        private readonly Subject<Collider2D> _onTriggerExit = new();

        [SerializeField]
        private Collider2D _collider = null!;
        
        public bool IsActive { get; private set; }

        public IObservable<Collider2D> OnTriggerEnter => _onTriggerEnter;
        public IObservable<Collider2D> OnTriggerExit => _onTriggerExit;

        private void Awake()
            => IsActive = _collider.enabled;

        public void Activate()
        {
            if (IsActive)
                return;
            _collider.enabled = true;
            IsActive = true;
        }

        public void Deactivate()
        {
            _collider.enabled = false;
            IsActive = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
            => _onTriggerEnter.OnNext(col);

        private void OnTriggerExit2D(Collider2D col)
            => _onTriggerExit.OnNext(col);
    }
}