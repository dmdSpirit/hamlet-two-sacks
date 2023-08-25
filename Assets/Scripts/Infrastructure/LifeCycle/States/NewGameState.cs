#nullable enable

using dmdspirit.Core;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class NewGameState : IState
    {
        public void Enter(StateMachine stateMachine, object? arg) { }

        public void Exit() { }
    }
}