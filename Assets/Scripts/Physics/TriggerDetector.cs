#nullable enable
using System;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Physics
{
    public sealed class TriggerDetector : MonoBehaviour
    {
        private readonly Subject<Collider2D> _onTriggerEnter = new();
        private readonly Subject<Collider2D> _onTriggerExit = new();

        public IObservable<Collider2D> OnTriggerEnter => _onTriggerEnter;
        public IObservable<Collider2D> OnTriggerExit => _onTriggerExit;

        private void OnTriggerEnter2D(Collider2D col)
            => _onTriggerEnter.OnNext(col);

        private void OnTriggerExit2D(Collider2D col)
            => _onTriggerExit.OnNext(col);
    }
}