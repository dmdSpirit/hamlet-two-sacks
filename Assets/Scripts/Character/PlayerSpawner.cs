#nullable enable

using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.Level;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Character
{
    public sealed class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {
        private CharactersManager _charactersManager = null!;
        private IPlayerFactory _playerFactory = null!;
        private LevelTransforms _levelTransforms = null!;

        // HACK (Stas): Temporary for testing.
        // - Stas 29 August 2023
        private VCTest _vcTest = null!;

        [SerializeField]
        private Transform _spawnPosition = null!;

        [Inject]
        private void Construct(CharactersManager charactersManager, IPlayerFactory playerFactory, VCTest vcTest,
            LevelTransforms levelTransforms)
        {
            _levelTransforms = levelTransforms;
            _vcTest = vcTest;
            _playerFactory = playerFactory;
            _charactersManager = charactersManager;
        }

        private void Awake()
            => _charactersManager.RegisterPlayerSpawner(this);

        private void OnDestroy()
            => _charactersManager.UnregisterPlayerSpawner(this);

        public PlayerBehaviour SpawnPlayer()
        {
            PlayerBehaviour playerBehaviour = _playerFactory.CreatePlayer();
            playerBehaviour.transform.SetParent(_levelTransforms.Units);
            playerBehaviour.transform.position = _spawnPosition.position;
            _vcTest.SetTarget(playerBehaviour.transform);
            return playerBehaviour;
        }
    }
}