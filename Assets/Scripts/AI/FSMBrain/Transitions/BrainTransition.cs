#nullable enable

using HamletTwoSacks.AI.FSMBrain.Decisions;
using HamletTwoSacks.AI.FSMBrain.States;
using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.Transitions
{
    [CreateAssetMenu(menuName = "BrainFSM/Transition", fileName = "transition", order = 0)]
    public sealed class BrainTransition : ScriptableObject
    {
        [SerializeField]
        private BrainDecision _decision = null!;

        [SerializeField]
        private BaseBrainState? _trueState;

        [SerializeField]
        private BaseBrainState? _falseState;

        public BaseBrainState? GetNextState(BrainFSM brain)
            => _decision.Decide(brain) ? _trueState : _falseState;
    }
}