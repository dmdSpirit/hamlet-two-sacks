#nullable enable

using HamletTwoSacks.Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

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

        [Inject]
        private void Construct(TimeController timeController)
            => timeController.FixedUpdate.Subscribe(OnFixedUpdate);

        private void OnEnable()
            => _moveAction.Enable();

        private void OnDisable()
            => _moveAction.Disable();

        private void OnFixedUpdate(Unit _)
        {
            var value = _moveAction.ReadValue<float>();
            _rigidbody2D.velocity = new Vector2(value * _speed * Time.fixedDeltaTime, _rigidbody2D.velocity.y);
            _spriteFlipper.FlipSprite(value);
        }
    }
}