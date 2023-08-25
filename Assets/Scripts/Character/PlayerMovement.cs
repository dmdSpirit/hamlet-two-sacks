#nullable enable

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HamletTwoSacks.Character
{
    public sealed class PlayerMovement : MonoBehaviour
    {
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
            transform.Translate(value * _speed * Time.fixedDeltaTime, 0, 0);
        }
    }
}