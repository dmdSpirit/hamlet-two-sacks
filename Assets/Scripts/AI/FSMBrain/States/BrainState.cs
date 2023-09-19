#nullable enable

using System.Collections.Generic;
using System.Linq;
using HamletTwoSacks.AI.FSMBrain.Actions;
using HamletTwoSacks.AI.FSMBrain.Transitions;
using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.States
{
    [CreateAssetMenu(menuName = "BrainFSM/State", fileName = "state", order = 0)]
    public sealed class BrainState : BaseBrainState
    {
        private readonly static Dictionary<BrainTransition, BaseBrainState> _executedTransitions = new();

        [SerializeField]
        private List<BrainAction> _actions = null!;

        [SerializeField]
        private List<BrainTransition> _transitions = null!;

        public override void Tick(BrainFSM brain, float time)
        {
            foreach (BrainAction action in _actions)
                action.Tick(brain, time);

            _executedTransitions.Clear();
            foreach (BrainTransition transition in _transitions)
            {
                BaseBrainState? nextState = transition.GetNextState(brain);
                if (nextState == null)
                    continue;
                _executedTransitions.Add(transition, nextState);
            }

            if (_executedTransitions.Count > 0)
                brain.LogMultipleTransitionsExecuted(_executedTransitions);
            KeyValuePair<BrainTransition, BaseBrainState> executedTransition = _executedTransitions.First();
            brain.SetState(executedTransition.Value, executedTransition.Key);
        }
    }
}