#nullable enable

using dmdspirit.Core;
using dmdspirit.Core.CommonInterfaces;
using dmdspirit.Core.UI;
using HamletTwoSacks.Characters;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Input;
using HamletTwoSacks.Time;
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
        private CrystalsManager _crystalsManager;

        public GameState(CharactersManager charactersManager, LevelManager levelManager,
            LoadingScreenShower loadingScreenShower, TimeController timeController, UIManager uiManager,
            IActionButtonsReader actionButtonsReader, CrystalsManager crystalsManager)
        {
            _crystalsManager = crystalsManager;
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
            _loadingScreenShower.ShowLoadingScreen();
            _uiManager.GetScreen<UIHud>().Hide();
            await _levelManager.UnloadCurrentLevel();
            _crystalsManager.Reset();

            _actionButtonsReader.Activate();
            _timeController.StopTime();
        }
    }
}