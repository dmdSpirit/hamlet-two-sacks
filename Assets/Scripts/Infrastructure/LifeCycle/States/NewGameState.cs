#nullable enable

using aether.Aether;
using aether.Aether.CommonInterfaces;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class NewGameState : IState
    {
        public void Enter(StateMachine stateMachine, object? arg)
        {
            // TODO (Stas): Initialize new game or load data.
            // - Stas 16 September 2023
            stateMachine.TriggerTransition();
        }

        public void Exit() { }
    }
}