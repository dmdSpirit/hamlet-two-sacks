#nullable enable

using HamletTwoSacks.AI.FSMBrain.Decisions;
using HamletTwoSacks.AI.FSMBrain.States;
using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.Transitions
{
    [CreateNodeMenu("Transition"), NodeTint("#420c1a")]
    public sealed class TransitionNode : BrainBaseNode
    {
        [SerializeField]
        private BrainDecision _decision = null!;

        [Output(connectionType: ConnectionType.Override), SerializeField]
        private StateNode? _trueState;

        [Output(connectionType: ConnectionType.Override), SerializeField]
        private StateNode? _falseState;

        public StateNode? GetNextState(BrainGraphFSM brain)
            => _decision.Decide(brain)
                   ? GetFirst<StateNode>(nameof(_trueState))
                   : GetFirst<StateNode>(nameof(_falseState));
    }
}