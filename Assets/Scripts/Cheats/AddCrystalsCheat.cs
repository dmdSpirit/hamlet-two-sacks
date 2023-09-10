#nullable enable

using HamletTwoSacks.Character;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace HamletTwoSacks.Cheats
{
    public sealed class AddCrystalsCheat : MonoBehaviour
    {
        private Player _player = null!;

        [SerializeField]
        private InputAction _action = null!;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            _action.performed += AddCrystal;
        }

        private void OnEnable()
            => _action.Enable();

        private void OnDisable()
            => _action.Disable();

        private void AddCrystal(InputAction.CallbackContext _)
            => _player.AddCrystal();
    }
}