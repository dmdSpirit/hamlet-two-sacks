#nullable enable

using System;
using System.Collections.Generic;
using HamletTwoSacks.AI.FSMBrain.States;
using HamletTwoSacks.AI.FSMBrain.Transitions;
using HamletTwoSacks.Time;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.AI.FSMBrain
{
    public class BrainGraphFSM : MonoBehaviour
    {
        private readonly Dictionary<Type, Component> _cachedComponents = new();

        private IDisposable _timeSub = null!;

        [SerializeField]
        private bool _log = false;

        [SerializeField]
        private BrainGraph _brainGraph = null!;

        public string BrainName { get; private set; } = null!;
        public StateNode CurrentBrainState { get; private set; } = null!;

        [Inject]
        private void Construct(TimeController timeController)
        {
            BrainName = $"[{nameof(BrainGraphFSM)}-{name}]";
            _timeSub = timeController.AITick.Subscribe(Tick);
        }

        private void Awake()
            => SetState(_brainGraph.InitialState);

        private void OnDestroy()
        {
            _timeSub.Dispose();
            _cachedComponents.Clear();
        }

        public void SetState(StateNode? state, TransitionNode? transition = null)
        {
            if (state == null)
            {
                Debug.LogError($"{BrainName} - trying to set [Null] state");
                return;
            }

            StateNode? previousState = CurrentBrainState;
            CurrentBrainState = state;
            if (!_log)
                return;
            string previousStateName = previousState == null ? "[Null]" : $"[{previousState.name}]";
            Debug.Log($"{BrainName} - from {previousStateName} {GetTransitionString(CurrentBrainState, transition)}.");
        }

        public void LogMultipleTransitionsExecuted(IReadOnlyDictionary<TransitionNode, StateNode> executedTransitions)
        {
            string currentStateName = CurrentBrainState == null ? "[Null]" : $"[{CurrentBrainState.name}]";
            Debug.LogError($"{BrainName} - multiple transitions executed from state {currentStateName}: {string.Join("\n", executedTransitions)}");
        }

        public T FindComponent<T>() where T : Component
        {
            Type type = typeof(T);
            if (_cachedComponents.TryGetValue(type, out Component? result))
                return (T)result;

            result = GetComponent<T>();
            if (result == null)
            {
                Debug.LogError($"{BrainName} - failed to find component of type {type.Name}");
                return null!;
            }

            _cachedComponents.Add(type, result);

            return (T)result;
        }

        private string GetTransitionString(StateNode toState, TransitionNode? transition = null)
        {
            string transitionName = transition == null ? "[No transition]" : $"[{transition.name}]";
            return $"to [{toState.name}] by {transitionName}";
        }

        private void Tick(float time)
            => CurrentBrainState.Tick(this, time);
    }
}