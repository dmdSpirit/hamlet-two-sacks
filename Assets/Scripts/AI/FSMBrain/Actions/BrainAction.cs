#nullable enable

using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.Actions
{
    public abstract class BrainAction : ScriptableObject
    {
        public abstract void Tick(BrainGraphFSM brain, float time);
    }
}