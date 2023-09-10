#nullable enable

using System;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Physics
{
    public sealed class CollisionDetector : MonoBehaviour
    {
        private readonly Subject<Collision2D> _onCollisionEnter = new();
        private readonly Subject<Collision2D> _onCollisionExit = new();

        public IObservable<Collision2D> OnCollisionEnter => _onCollisionEnter;
        public IObservable<Collision2D> OnCollisionExit => _onCollisionExit;

        private void OnCollisionEnter2D(Collision2D col)
            => _onCollisionEnter.OnNext(col);

        private void OnCollisionExit2D(Collision2D col)
            => _onCollisionExit.OnNext(col);
    }
}