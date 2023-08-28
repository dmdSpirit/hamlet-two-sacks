#nullable enable

using dmdspirit.Core;
using JetBrains.Annotations;
using UnityEngine;

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
            if (arg == null)
            {
                Debug.LogError($"{nameof(LoadLevelState)} got unexpected null argument");
                return;
            }

            if (arg is not int sceneIndex)
            {
                Debug.LogError($"{nameof(LoadLevelState)} got unexpected argument of type {arg.GetType()}");
                return;
            }

            _sceneIndex = sceneIndex;
            await _sceneLoader.LoadSceneAdditive(sceneIndex);
        }

        public void Exit() { }
    }
}