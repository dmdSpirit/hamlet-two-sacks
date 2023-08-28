#nullable enable

using JetBrains.Annotations;
using UnityEngine;

namespace HamletTwoSacks.Character
{
    [UsedImplicitly]
    public sealed class CharactersManager : ICharactersManager
    {
        // private readonly IPlayerSpawner _playerSpawner;
        
        public Player? Player { get; private set; }

        // TODO (Stas): 28 August 2023
        // - Stas Handle this.
        
        // public CharactersManager(IPlayerSpawner playerSpawner)
        // {
        //     _playerSpawner = playerSpawner;
        // }

        public void SpawnPlayer()
        {
            // if (Player != null)
            //     return;
            // Player = _playerSpawner.SpawnPlayer();
            Debug.Log($"Player spawned.");
        }
    }
}