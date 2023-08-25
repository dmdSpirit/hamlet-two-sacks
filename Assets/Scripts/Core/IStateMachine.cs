#nullable enable
using System;

namespace dmdspirit.Core
{
    public interface IStateMachine
    {
        void Start(IState initialState, object? arg = default);
        void Stop();
        void RegisterState<TState>(IState state) where TState : class, IState;
        IState? GetState<TState>() where TState : class, IState;
        void AddTransition(IState state, Func<IState?, object?, IState?> transition);
        void TriggerTransition(IState? toState = default, object? arg = default);
        IState? ToState<TState>(IState? toState, object? arg) where TState : class, IState;
        IState? ToState<TState, TArg>(IState? toState, object? arg) where TState : class, IState;
        IState? NextState<TState>(IState? toState, object? arg) where TState : class, IState;
    }
}