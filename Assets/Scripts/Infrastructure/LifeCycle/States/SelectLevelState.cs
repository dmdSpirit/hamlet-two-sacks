#nullable enable

using aether.Aether;
using aether.Aether.CommonInterfaces;
using aether.Aether.UI;
using HamletTwoSacks.GameMap;
using HamletTwoSacks.UI;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class SelectLevelState : IState
    {
        private readonly UIManager _uiManager;
        private readonly LoadingScreenShower _loadingScreenShower;

        public SelectLevelState(UIManager uiManager, LoadingScreenShower loadingScreenShower)
        {
            _loadingScreenShower = loadingScreenShower;
            _uiManager = uiManager;
        }

        public void Enter(StateMachine stateMachine, object? arg)
            => _uiManager.GetScreen<MapScreen>().Show();

        public void Exit()
        {
            _loadingScreenShower.ShowLoadingScreen();
            _uiManager.GetScreen<MapScreen>().Hide();
        }
    }
}