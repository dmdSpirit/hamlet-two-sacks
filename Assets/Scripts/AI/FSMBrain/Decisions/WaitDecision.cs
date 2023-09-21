#nullable enable

using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.Decisions
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Wait", fileName = "wait decision", order = 0)]
    public sealed class WaitDecision : BrainDecision
    {
        private float _timePassed;

        [SerializeField]
        private float _cooldown;

        public override bool Decide(BrainGraphFSM brain)
        {
            _timePassed += UnityEngine.Time.deltaTime;
            if (_timePassed < _cooldown)
                return false;

            _timePassed -= _cooldown;
            return true;
        }
    }
}