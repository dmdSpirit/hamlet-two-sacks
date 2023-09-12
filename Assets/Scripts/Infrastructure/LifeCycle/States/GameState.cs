﻿#nullable enable

using dmdspirit.Core;
using dmdspirit.Core.CommonInterfaces;
using dmdspirit.Core.UI;
using HamletTwoSacks.Character;
using HamletTwoSacks.Infrastructure.Time;
using HamletTwoSacks.Input;
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
        private readonly TimeController _timeController;
        private readonly UIManager _uiManager;
        private readonly IActionButtonsReader _actionButtonsReader;

        public GameState(CharactersManager charactersManager, LevelManager levelManager,
            LoadingScreenShower loadingScreenShower, TimeController timeController, UIManager uiManager, IActionButtonsReader actionButtonsReader)
        {
            _actionButtonsReader = actionButtonsReader;
            _uiManager = uiManager;
            _timeController = timeController;

            _loadingScreenShower = loadingScreenShower;
            _levelManager = levelManager;
            _charactersManager = charactersManager;
        }

        public async void Enter(StateMachine stateMachine, object? arg)
        {
            _loadingScreenShower.ShowLoadingScreen();
            var sceneIndex = (int)arg!;
            await _levelManager.LoadLevel(sceneIndex);
            _charactersManager.SpawnPlayer();

            _timeController.StartTime();
            _loadingScreenShower.HideLoadingScreen();
            _uiManager.GetScreen<UIHud>().Show();
            _actionButtonsReader.Activate();
        }

        public async void Exit()
        {
            _uiManager.GetScreen<UIHud>().Hide();
            _loadingScreenShower.ShowLoadingScreen();
            await _levelManager.UnloadCurrentLevel();

            _actionButtonsReader.Activate();
            _timeController.StopTime();
        }
    }
}