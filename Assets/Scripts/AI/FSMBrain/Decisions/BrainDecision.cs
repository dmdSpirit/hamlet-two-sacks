#nullable enable

using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.Decisions
{
    public abstract class BrainDecision : ScriptableObject
    {
        public abstract bool Decide(BrainFSM brainFsm);
    }
}