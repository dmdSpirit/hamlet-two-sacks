#nullable enable

using JetBrains.Annotations;
using UnityEngine;

namespace HamletTwoSacks.Character
{
    [UsedImplicitly]
    public sealed class CharactersManager
    {
        private IPlayerSpawner? _playerSpawner;

        public Player? Player { get; private set; }

        public void SpawnPlayer()
        {
            _playerSpawner!.SpawnPlayer();
        }

        public void RegisterPlayerSpawner(IPlayerSpawner playerSpawner)
        {
            if (_playerSpawner != null)
            {
                Debug.LogError($"Trying to register multiple player spawners.");
                return;
            }

            _playerSpawner = playerSpawner;
        }

        public void UnregisterPlayerSpawner(IPlayerSpawner playerSpawner)
        {
            if (_playerSpawner != playerSpawner)
            {
                Debug.LogError($"Trying to unregister player spawner, but it does not match the registered one.");
                return;
            }

            _playerSpawner = null;
        }
    }
}