#nullable enable
using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.Actions
{
    [CreateAssetMenu(menuName = "FSM/Actions/Timed Log", fileName = "timed log action", order = 0)]
    public sealed class TimedLogAction : BrainAction
    {
        private float _timePassed;
        
        [SerializeField]
        private float _cooldown;
        
        public override void Tick(BrainGraphFSM brain, float time)
        {
            _timePassed += UnityEngine.Time.deltaTime;
            if (_timePassed < _cooldown)
                return;

            _timePassed -= _cooldown;
            Debug.Log($"{name} - Tick");
        }
    }
}