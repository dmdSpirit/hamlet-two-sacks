#nullable enable

using aether.Aether;
using aether.Aether.CommonInterfaces;
using HamletTwoSacks.UI;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class InitializeGameState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreenShower _loadingScreenShower;

        public InitializeGameState(SceneLoader sceneLoader, LoadingScreenShower loadingScreenShower)
        {
            _loadingScreenShower = loadingScreenShower;
            _sceneLoader = sceneLoader;
        }

        public async void Enter(StateMachine stateMachine, object? arg)
        {
            _loadingScreenShower.ShowLoadingScreen();
            await _sceneLoader.LoadSceneAdditive(1);
            stateMachine.TriggerTransition();
        }

        public void Exit() { }
    }
}