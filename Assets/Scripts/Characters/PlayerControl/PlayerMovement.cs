#nullable enable

using HamletTwoSacks.Time;
using HamletTwoSacks.TwoD;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace HamletTwoSacks.Characters.PlayerControl
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

        private void OnFixedUpdate(float time)
        {
            var value = _moveAction.ReadValue<float>();
            _rigidbody2D.velocity = new Vector2(value * _speed * time, _rigidbody2D.velocity.y);
            _spriteFlipper.FlipSprite(value);
        }
    }
}