#nullable enable

using dmdspirit.Core;
using dmdspirit.Core.CommonInterfaces;
using dmdspirit.Core.UI;
using HamletTwoSacks.Characters.PlayerControl;
using HamletTwoSacks.Input;
using HamletTwoSacks.Time;
using HamletTwoSacks.UI;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class GameState : IState
    {
        private readonly LevelManager _levelManager;
        private readonly LoadingScreenShower _loadingScreenShower;
        private readonly TimeController _timeController;
        private readonly UIManager _uiManager;
        private readonly IActionButtonsReader _actionButtonsReader;
        private readonly PlayerManager _playerManager;

        public GameState(LevelManager levelManager,
            LoadingScreenShower loadingScreenShower, TimeController timeController, UIManager uiManager,
            IActionButtonsReader actionButtonsReader, PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _actionButtonsReader = actionButtonsReader;
            _uiManager = uiManager;
            _timeController = timeController;

            _loadingScreenShower = loadingScreenShower;
            _levelManager = levelManager;
        }

        public async void Enter(StateMachine stateMachine, object? arg)
        {
            _loadingScreenShower.ShowLoadingScreen();
            var sceneIndex = (int)arg!;
            await _levelManager.LoadLevel(sceneIndex);

            _timeController.StartTime();
            _playerManager.SpawnPlayer();
            _loadingScreenShower.HideLoadingScreen();
            _uiManager.GetScreen<UIHud>().Show();
            _actionButtonsReader.Activate();
        }

        public async void Exit()
        {
            _loadingScreenShower.ShowLoadingScreen();
            _uiManager.GetScreen<UIHud>().Hide();
            await _levelManager.UnloadCurrentLevel();

            _actionButtonsReader.Activate();
            _timeController.StopTime();
        }
    }
}