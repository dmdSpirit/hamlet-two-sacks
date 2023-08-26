#nullable enable
using dmdspirit.Core.Attributes;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Character
{
    public sealed class PlayerSpawner : MonoBehaviour
    {
        private CharacterFactory _characterFactory = null!;

        [SerializeField]
        private Transform _spawnPosition = null!;

        [SerializeField, Button(nameof(SpawnPlayer))]
        private bool _spawnPlayer;

        [Inject]
        private void Construct(CharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
        }

        public Player SpawnPlayer()
        {
            Player player = _characterFactory.CreatePlayer();
            player.transform.position = _spawnPosition.position;
            return player;
        }
    }
}