#nullable enable

using System.Collections.Generic;
using System.Linq;
using HamletTwoSacks.AI.FSMBrain.Actions;
using HamletTwoSacks.AI.FSMBrain.Transitions;
using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.States
{
    [CreateNodeMenu("State"), NodeTint("#0e1c0f")]
    public sealed class StateNode : BrainBaseNode
    {
        private readonly static Dictionary<TransitionNode, StateNode> _executedTransitions = new();

        [SerializeField]
        private List<BrainAction> _actions = null!;

        [Output, SerializeField]
        private List<TransitionNode> _transitions = null!;

        public void Tick(BrainGraphFSM brain, float time)
        {
            foreach (BrainAction action in _actions)
                action.Tick(brain, time);

            _executedTransitions.Clear();
            IEnumerable<TransitionNode> transitions = GetAllOnPort<TransitionNode>(nameof(_transitions));
            foreach (TransitionNode transition in transitions)
            {
                StateNode? nextState = transition.GetNextState(brain);
                if (nextState == null)
                    continue;
                _executedTransitions.Add(transition, nextState);
            }

            if (_executedTransitions.Count == 0)
                return;
            if (_executedTransitions.Count > 1)
                brain.LogMultipleTransitionsExecuted(_executedTransitions);
            KeyValuePair<TransitionNode, StateNode> executedTransition = _executedTransitions.First();
            brain.SetState(executedTransition.Value, executedTransition.Key);
        }
    }
}