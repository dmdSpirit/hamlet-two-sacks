#nullable enable

using dmdspirit.Core;
using dmdspirit.Core.UI;
using HamletTwoSacks.UI;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class MainMenuState : IState
    {
        private readonly IUIManager _uiManager;
        private readonly LoadingScreenShower _loadingScreenShower;

        private MainMenuScreen _mainMenu = null!;

        public MainMenuState(IUIManager uiManager, LoadingScreenShower loadingScreenShower)
        {
            _loadingScreenShower = loadingScreenShower;
            _uiManager = uiManager;
        }

        public void Enter(StateMachine stateMachine, object? arg)
        {
            _loadingScreenShower.HideLoadingScreen();
            _mainMenu = _uiManager.GetScreen<MainMenuScreen>();
            _mainMenu.Show();
            stateMachine.TriggerTransition(stateMachine.GetState<NewGameState>());
        }

        public void Exit()
            => _mainMenu.Hide();
    }
}