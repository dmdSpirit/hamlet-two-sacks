#nullable enable

using HamletTwoSacks.TwoD;
using UnityEngine;

namespace HamletTwoSacks.AI
{
    public sealed class FlyHorizontallyToTargetTask : Task
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _completionRadius;

        [SerializeField]
        private Rigidbody2D _rigidbody2D = null!;

        [SerializeField]
        private Transform _target = null!;

        [SerializeField]
        private SpriteFlipper _spriteFlipper = null!;

        protected override void OnActivate() { }

        protected override void OnDeactivate() { }

        protected override void OnComplete() { }

        public override void OnFixedUpdate(float time)
        {
            if (!IsActive)
                return;

            float currentPosition = _rigidbody2D.transform.position.x;
            float destination = _target.position.x;
            if (Mathf.Abs(currentPosition - destination) <= _completionRadius)
            {
                Complete();
                return;
            }

            float direction = Mathf.Sign(destination - currentPosition);
            _spriteFlipper.FlipSprite(direction);
            _rigidbody2D.velocity = new Vector2(direction * _speed * time, 0);
        }

        public override void OnUpdate(float time) { }
    }
}