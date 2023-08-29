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
        private readonly CameraTargetFollow _cameraTargetFollow;
        private readonly TimeController _timeController;

        public GameState(CharactersManager charactersManager, LevelManager levelManager,
            LoadingScreenShower loadingScreenShower, CameraTargetFollow cameraTargetFollow,
            TimeController timeController)
        {
            _timeController = timeController;
            _cameraTargetFollow = cameraTargetFollow;
            _loadingScreenShower = loadingScreenShower;
            _levelManager = levelManager;
            _charactersManager = charactersManager;
        }

        public async void Enter(StateMachine stateMachine, object? arg)
        {
            var sceneIndex = (int)arg!;
            await _levelManager.LoadLevel(sceneIndex);
            _charactersManager.SpawnPlayer();
            _cameraTargetFollow.FocusImmediately();
            _cameraTargetFollow.StartFollow();
            _timeController.StartTime();
            _loadingScreenShower.HideLoadingScreen();
        }

        public async void Exit()
        {
            _loadingScreenShower.ShowLoadingScreen();
            await _levelManager.UnloadCurrentLevel();
            _cameraTargetFollow.StopFollow();
            _timeController.StopTime();
        }
    }
}