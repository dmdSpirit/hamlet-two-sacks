﻿#nullable enable

using HamletTwoSacks.Characters.PlayerControl;
using JetBrains.Annotations;
using UnityEngine;

namespace HamletTwoSacks.Characters
{
    [UsedImplicitly]
    public sealed class CharactersManager
    {
        private IPlayerSpawner? _playerSpawner;

        public PlayerBehaviour? Player { get; private set; }

        public void SpawnPlayer()
            => Player = _playerSpawner!.SpawnPlayer();

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