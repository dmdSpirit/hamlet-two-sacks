#nullable enable

using dmdspirit.Core;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class InitializeGameState : IState
    {
        public void Enter(StateMachine stateMachine, object? arg)
            => stateMachine.TriggerTransition();

        public void Exit() { }
    }
}