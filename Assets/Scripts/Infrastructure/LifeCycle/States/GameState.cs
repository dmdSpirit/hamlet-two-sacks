#nullable enable
using dmdspirit.Core;
using HamletTwoSacks.Character;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class GameState : IState
    {
        private readonly CharactersManager _charactersManager;

        public GameState(CharactersManager charactersManager)
        {
            _charactersManager = charactersManager;
        }

        public void Enter(StateMachine stateMachine, object? arg)
        {
            _charactersManager.SpawnPlayer();
        }

        public void Exit() { }
    }
}