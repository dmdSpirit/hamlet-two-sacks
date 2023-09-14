#nullable enable
using System;
using System.Linq;
using HamletTwoSacks.Infrastructure;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Characters.PlayerControl
{
    [UsedImplicitly]
    public sealed class PlayerManager
    {
        private readonly EntityManager _entityManager;
        private readonly Player _player;

        private readonly Subject<PlayerBehaviour> _onPlayerSpawned = new();

        private PlayerBehaviour? _playerBehaviour;

        public IObservable<PlayerBehaviour> OnPlayerSpawned => _onPlayerSpawned;

        public PlayerManager(EntityManager entityManager, Player player)
        {
            _player = player;
            _entityManager = entityManager;
        }

        public void SpawnPlayer()
        {
            Spawner<PlayerBehaviour>? playerSpawner = _entityManager.GetSpawners<PlayerBehaviour>().FirstOrDefault();
            if (playerSpawner == null)
            {
                Debug.LogError($"No player spawner is registered to {nameof(EntityManager)}");
                return;
            }

            _playerBehaviour = playerSpawner.Spawn();
            _onPlayerSpawned.OnNext(_playerBehaviour);
        }

        public void DestroyPlayer()
        {
            if (_playerBehaviour != null)
                _entityManager.DestroyObject(_playerBehaviour);
            _player.Reset();
        }
    }
}