#nullable enable

using aether.Aether;
using aether.Aether.CommonInterfaces;
using aether.Aether.UI;
using HamletTwoSacks.UI;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class MainMenuState : IState
    {
        private readonly UIManager _uiManager;
        private readonly LoadingScreenShower _loadingScreenShower;

        private MainMenuScreen _mainMenu = null!;

        public MainMenuState(UIManager uiManager, LoadingScreenShower loadingScreenShower)
        {
            _loadingScreenShower = loadingScreenShower;
            _uiManager = uiManager;
        }

        public void Enter(StateMachine stateMachine, object? arg)
        {
            _loadingScreenShower.HideLoadingScreen();
            _mainMenu = _uiManager.GetScreen<MainMenuScreen>();
            _mainMenu.Show();
        }

        public void Exit()
        {
            _mainMenu.Hide();
        }
    }
}