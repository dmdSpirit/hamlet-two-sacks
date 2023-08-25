#nullable enable
namespace dmdspirit.Core
{
    public interface IState
    {
        void Enter(StateMachine stateMachine, object? arg);
        void Exit();
    }
}