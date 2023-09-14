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

        private readonly Subject<PlayerBehaviour> _onPlayerSpawned = new();

        public IObservable<PlayerBehaviour> OnPlayerSpawned => _onPlayerSpawned;

        public PlayerManager(EntityManager entityManager)
            => _entityManager = entityManager;

        public void SpawnPlayer()
        {
            Spawner<PlayerBehaviour>? playerSpawner = _entityManager.GetSpawners<PlayerBehaviour>().FirstOrDefault();
            if (playerSpawner == null)
            {
                Debug.LogError($"No player spawner is registered to {nameof(EntityManager)}");
                return;
            }

            PlayerBehaviour player = playerSpawner.Spawn();
            _onPlayerSpawned.OnNext(player);
        }
    }
}