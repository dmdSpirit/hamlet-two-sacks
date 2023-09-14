#nullable enable

using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.TwoD;
using UnityEngine;

namespace HamletTwoSacks.AI
{
    public sealed class FlyHorizontallyToTargetTask : Task
    {
        private Rigidbody2D _rigidbody2D = null!;
        private SpriteFlipper _spriteFlipper = null!;
        
        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _completionRadius;

        [SerializeField]
        private Transform _target = null!;

        public override void Initialize(SystemReferences references)
        {
            _spriteFlipper = references.GetSystemWithCheck<SpriteFlipper>();
            _rigidbody2D = references.GetSystemWithCheck<Rigidbody2D>();
        }

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

        protected override void OnActivate() { }

        protected override void OnDeactivate() { }

        protected override void OnComplete()
        {
            // FIXME (Stas): No point in setting velocity every frame.
            // - Stas 14 September 2023
            
            // FIXME (Stas): Not sure if this would correctly work with game pause through TimeController.
            // - Stas 14 September 2023
            _rigidbody2D.velocity = new Vector2(0, 0);
        }
    }
}