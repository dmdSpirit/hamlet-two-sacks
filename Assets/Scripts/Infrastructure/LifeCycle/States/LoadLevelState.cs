#nullable enable

using dmdspirit.Core;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class LoadLevelState : IState
    {
        private readonly SceneLoader _sceneLoader;

        private int _sceneIndex;

        public LoadLevelState(SceneLoader sceneLoader)
            => _sceneLoader = sceneLoader;

        public async void Enter(StateMachine stateMachine, object? arg)
        {
            _sceneIndex = (int)arg!;
            await _sceneLoader.LoadSceneAdditive(_sceneIndex);
            stateMachine.TriggerTransition();
        }

        public void Exit() { }
    }
}