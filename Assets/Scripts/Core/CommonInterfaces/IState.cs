#nullable enable
namespace dmdspirit.Core.CommonInterfaces
{
    public interface IState
    {
        void Enter(StateMachine stateMachine, object? arg);
        void Exit();
    }
}