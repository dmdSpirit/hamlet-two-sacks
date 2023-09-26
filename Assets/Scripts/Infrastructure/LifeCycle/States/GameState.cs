#nullable enable

using System;
using Aether;
using Aether.CommonInterfaces;
using Aether.UI;
using Cysharp.Threading.Tasks;
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

        public GameState(LevelManager levelManager, LoadingScreenShower loadingScreenShower,
            TimeController timeController, UIManager uiManager, IActionButtonsReader actionButtonsReader,
            PlayerManager playerManager)
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
            // TODO (Stas): Refactor level loading into separate state.
            // - Stas 16 September 2023
            _loadingScreenShower.ShowLoadingScreen();
            var sceneIndex = (int)arg!;

            // HACK (Stas): showing loading screen for at least 2 seconds, to deal with screen flickering.
            // - Stas 14 September 2023
            await UniTask.WhenAll(LoadLevel(sceneIndex), UniTask.Delay(TimeSpan.FromSeconds(1)));

            _timeController.StartTime();

            _loadingScreenShower.HideLoadingScreen();
            _uiManager.GetScreen<UIHud>().Show();
            _actionButtonsReader.Activate();
        }

        public async void Exit()
        {
            _loadingScreenShower.ShowLoadingScreen();
            _uiManager.GetScreen<UIHud>().Hide();
            _playerManager.DestroyPlayer();
            await _levelManager.UnloadCurrentLevel();

            _actionButtonsReader.Activate();
            _timeController.StopTime();
        }

        private async UniTask LoadLevel(int sceneIndex)
        {
            await _levelManager.LoadLevel(sceneIndex);
            _playerManager.SpawnPlayer();
        }
    }
}