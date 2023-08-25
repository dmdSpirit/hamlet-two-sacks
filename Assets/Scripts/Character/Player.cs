#nullable enable

using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Character
{
    [RequireComponent(typeof(PlayerMovement))]
    public sealed class Player : MonoBehaviour, IInitializable
    {
        private PlayerMovement _playerMovement = null!;

        public void Initialize()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }
    }
}