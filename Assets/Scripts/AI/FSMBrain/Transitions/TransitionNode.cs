#nullable enable

using HamletTwoSacks.AI.FSMBrain.Decisions;
using HamletTwoSacks.AI.FSMBrain.States;
using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.Transitions
{
    public sealed class TransitionNode : FSMBaseNode
    {
        [SerializeField]
        private BrainDecision _decision = null!;

        [Output]
        private StateNode? _trueState;

        [Output]
        private StateNode? _falseState;

        public StateNode? GetNextState(BrainGraphFSM brain)
            => _decision.Decide(brain)
                   ? GetFirst<StateNode>(nameof(_trueState))
                   : GetFirst<StateNode>(nameof(_falseState));
    }
}