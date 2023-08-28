#nullable enable
using dmdspirit.Core;
using HamletTwoSacks.Character;
using HamletTwoSacks.UI;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class GameState : IState
    {
        private readonly CharactersManager _charactersManager;
        private readonly LevelManager _levelManager;
        private readonly LoadingScreenShower _loadingScreenShower;

        public GameState(CharactersManager charactersManager, LevelManager levelManager,
            LoadingScreenShower loadingScreenShower)
        {
            _loadingScreenShower = loadingScreenShower;
            _levelManager = levelManager;
            _charactersManager = charactersManager;
        }

        public async void Enter(StateMachine stateMachine, object? arg)
        {
            _loadingScreenShower.HideLoadingScreen();
            var sceneIndex = (int)arg!;
            await _levelManager.LoadLevel(sceneIndex);
            _charactersManager.SpawnPlayer();
        }

        public async void Exit()
        {
            _loadingScreenShower.ShowLoadingScreen();
            await _levelManager.UnloadCurrentLevel();
        }
    }
}