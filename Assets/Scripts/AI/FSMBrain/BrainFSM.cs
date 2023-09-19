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
    public sealed class BrainFSM : MonoBehaviour
    {
        private IDisposable _timeSub = null!;
        private string _fsmName = null!;

        [SerializeField]
        private bool _log;

        [SerializeField]
        private BaseBrainState _initialBrainState = null!;

        public BaseBrainState CurrentBrainState { get; private set; } = null!;

        [Inject]
        private void Construct(TimeController timeController)
        {
            _fsmName = $"[{nameof(BrainFSM)}-{name}]";
            _timeSub = timeController.AITick.Subscribe(Tick);
        }

        private void Awake()
        {
            if (_initialBrainState == null)
                Debug.LogError($"{_fsmName} does not have initial state set.");
            if (_log)
                Debug.Log($"{_fsmName} - initializing");
            SetState(_initialBrainState!);
        }

        public void SetState(BaseBrainState state, BrainTransition? transition = null)
        {
            BaseBrainState? previousState = CurrentBrainState;
            CurrentBrainState = _initialBrainState;
            if (!_log)
                return;
            string previousStateName = previousState == null ? "[Null]" : $"[{previousState.name}]";
            Debug.Log($"{_fsmName} - from {previousStateName} {GetTransitionString(CurrentBrainState, transition)}.");
        }

        public void LogMultipleTransitionsExecuted(
            IReadOnlyDictionary<BrainTransition, BaseBrainState> executedTransitions)
        {
            string currentStateName = CurrentBrainState == null ? "[Null]" : $"[{CurrentBrainState.name}]";
            Debug.LogError($"{_fsmName} - multiple transitions executed from state {currentStateName}: {string.Join("\n", executedTransitions)}");
        }

        private string GetTransitionString(BaseBrainState toState, BrainTransition? transition = null)
        {
            string transitionName = transition == null ? "[No transition]" : $"[{transition.name}]";
            return $"to [{toState.name}] by {transitionName}";
        }

        private void Tick(float time)
            => CurrentBrainState.Tick(this, time);
    }
}