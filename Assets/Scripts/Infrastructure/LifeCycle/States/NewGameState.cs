#nullable enable

using dmdspirit.Core;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class NewGameState : IState
    {
        public void Enter(StateMachine stateMachine, object? arg)
        {
            // HACK (Stas): Hardcoded scene index.
            // - Stas 28 August 2023
            stateMachine.TriggerTransition(stateMachine.GetState<GameState>(), 1);
        }

        public void Exit() { }
    }
}