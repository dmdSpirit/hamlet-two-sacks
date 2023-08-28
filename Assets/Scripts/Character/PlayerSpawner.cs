#nullable enable
using dmdspirit.Core.Attributes;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Character
{
    public sealed class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {
        private IPlayerFactory _playerFactory = null!;

        [SerializeField]
        private Transform _spawnPosition = null!;

        [SerializeField, Button(nameof(SpawnPlayer))]
        private bool _spawnPlayer;

        [Inject]
        private void Construct(IPlayerFactory playerFactory)
            => _playerFactory = playerFactory;

        public Player SpawnPlayer()
        {
            Player player = _playerFactory.CreatePlayer();
            player.transform.position = _spawnPosition.position;
            return player;
        }
    }
}