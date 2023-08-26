#nullable enable

using UnityEngine;
using UnityEngine.InputSystem;

namespace HamletTwoSacks.Character
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D = null!;

        [SerializeField]
        private SpriteFlipper _spriteFlipper = null!;

        [SerializeField]
        private InputAction _moveAction = null!;

        [SerializeField]
        private float _speed;

        private void OnEnable()
            => _moveAction.Enable();

        private void OnDisable()
            => _moveAction.Disable();

        private void FixedUpdate()
        {
            if (!_moveAction.IsInProgress())
                return;
            var value = _moveAction.ReadValue<float>();
            _rigidbody2D.velocity = new Vector2(value * _speed * Time.fixedDeltaTime, _rigidbody2D.velocity.y);
            _spriteFlipper.FlipSprite(value);
        }
    }
}