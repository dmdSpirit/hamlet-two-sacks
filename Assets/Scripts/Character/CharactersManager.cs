#nullable enable

using JetBrains.Annotations;

namespace HamletTwoSacks.Character
{
    [UsedImplicitly]
    public sealed class CharactersManager
    {
        private readonly PlayerSpawner _playerSpawner;
        
        public Player? Player { get; private set; }

        public CharactersManager(PlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
        }

        public void SpawnPlayer()
        {
            if (Player != null)
                return;
            Player = _playerSpawner.SpawnPlayer();
        }
    }
}