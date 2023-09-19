#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.States
{
    public abstract class BaseBrainState : ScriptableObject
    {
        public abstract void Tick(BrainFSM brain, float time);



    }
}