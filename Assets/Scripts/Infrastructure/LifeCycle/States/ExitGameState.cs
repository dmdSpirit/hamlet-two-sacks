#nullable enable

using dmdspirit.Core;
using dmdspirit.Core.AssetManagement;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public class ExitGameState : IState
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IGameQuitter _gameQuitter;

        public ExitGameState(IAssetProvider assetProvider, IGameQuitter gameQuitter)
        {
            _gameQuitter = gameQuitter;
            _assetProvider = assetProvider;
        }

        public void Enter(StateMachine stateMachine, object? arg)
        {
            _assetProvider.CleanUp();
            _gameQuitter.QuitGame();
        }

        public void Exit() { }
    }
}