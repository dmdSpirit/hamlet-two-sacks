#nullable enable
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace dmdspirit.Core
{
    public class StateMachine
    {
        private readonly bool _verboseStateSwitch;
        private readonly string _name;

        private readonly Dictionary<IState, List<Func<IState?, object?, IState?>>> _transitions = new();
        private readonly Dictionary<Type, IState> _states = new();
        private readonly Subject<IState> _onStateChange = new();

        private IState? _currentState;

        public IObservable<IState> OnStateChange => _onStateChange;

        public StateMachine(string name = "", bool verboseStateSwitch = false)
        {
            _name = name;
            _verboseStateSwitch = verboseStateSwitch;
        }

        public void Start(IState initialState, object? arg = default)
        {
            if (_currentState != null)
            {
                Debug.LogError($"{GetMachineName()} Trying to start state machine that is already running and currently on state {GetTypeName(_currentState)}.");
                return;
            }

            if (_verboseStateSwitch)
                Debug.Log($"{GetMachineName()} Starting at {GetTypeName(initialState)}");

            EnterState(initialState, arg);
        }

        public void Stop()
        {
            if (_currentState == null)
                return;

            if (_verboseStateSwitch)
                Debug.Log($"{GetMachineName()} Stopping at {GetTypeName(_currentState)}");

            _currentState.Exit();
            _currentState = null;
        }

        public void RegisterState<TState>(IState state) where TState : class, IState
        {
            if (_states.ContainsKey(typeof(TState)))
            {
                Debug.LogError($"{GetMachineName()} State machine already has state of type {typeof(TState).Name} registered.");
                return;
            }

            _states.Add(state.GetType(), state);
        }

        public IState? GetState<TState>() where TState : class, IState
        {
            if (_states.TryGetValue(typeof(TState), out IState state))
                return state;
            Debug.LogError($"{GetMachineName()} No states of type {typeof(TState).Name} registered.");
            return null;
        }

        public void AddTransition(IState state, Func<IState?, object?, IState?> transition)
        {
            if (!_transitions.ContainsKey(state))
                _transitions.Add(state, new List<Func<IState?, object?, IState?>>());
            _transitions[state].Add(transition);
        }

        public void TriggerTransition(IState? toState = default, object? arg = default)
        {
            if (_currentState == null)
            {
                Debug.LogError($"{GetMachineName()} Trying to trigger transition, but state machine has not started.");
                return;
            }

            if (!_transitions.TryGetValue(_currentState, out List<Func<IState?, object?, IState?>>? transitions))
            {
                Debug.LogError($"{GetMachineName()} No registered transitions for {GetTypeName(_currentState)}.");
                return;
            }

            foreach (Func<IState?, object?, IState?> transition in transitions)
            {
                IState? nextState = transition.Invoke(toState, arg);
                if (nextState == null)
                    continue;
                EnterState(nextState, arg);
                return;
            }

            Debug.LogError($"{GetMachineName()} No transitions from state {GetTypeName(_currentState)} to ({GetTypeName(toState)}) with argument of type ({GetTypeName(arg)}).");
        }

        public IState? ToState<TState>(IState? toState, object? arg) where TState : class, IState
            => toState is TState ? toState : null;

        public IState? NextState<TState>(IState? toState, object? arg) where TState : class, IState
            => toState is null or TState ? GetState<TState>() : null;

        public IState? ToState<TState, TArg>(IState? toState, object? arg) where TState : class, IState
            => toState is TState && arg is TArg ? toState : null;

        private void EnterState(IState state, object? arg)
        {
            PrintStatus(state, arg);
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter(this, arg);
            _onStateChange.OnNext(_currentState);
        }

        private void PrintStatus(IState state, object? arg)
        {
            if (!_verboseStateSwitch
                || _currentState == null)
                return;
            Debug.Log($"{GetMachineName()} Transition {GetTypeName(_currentState)} => {GetTypeName(state)} with argument of type ({GetTypeName(arg)})");
        }

        private string GetMachineName()
            => string.IsNullOrEmpty(_name) ? "[State Machine]:" : $"[State Machine] ({_name}):";

        private string GetTypeName(object? state)
            => state == null ? "null" : state.GetType().Name;
    }
}