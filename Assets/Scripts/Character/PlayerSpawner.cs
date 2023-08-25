#nullable enable
using dmdspirit.Core.Attributes;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Character
{
    public sealed class PlayerSpawner : MonoBehaviour
    {
        private CharacterFactory _characterFactory = null!;

        private Player? _player;
        
        [SerializeField]
        private Transform _spawnPosition = null!;

        [SerializeField, Button(nameof(SpawnPlayer))]
        private bool _spawnPlayer;

        [Inject]
        private void Construct(CharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
        }

        private void SpawnPlayer()
        {
            if (_player != null)
            {
                Debug.LogWarning($"Player is already spawned", _player);
                return;
            }

            _player = _characterFactory.CreatePlayer();
            _player.transform.position = _spawnPosition.position;
        }
    }
}