#nullable enable

using UnityEngine;

namespace HamletTwoSacks.AI.FSMBrain.States
{
    [CreateNodeMenu("Initial Node"), NodeTint("#163216"), DisallowMultipleNodes]
    public class InitialStateNode : BrainBaseNode
    {
        private StateNode? _node;

        [Output(connectionType: ConnectionType.Override), SerializeField]
        private StateNode? _initialNode;

        public StateNode? GetNextNode()
        {
            if (_node == null)
                _node = GetFirst<StateNode>(nameof(_initialNode));
            return _node;
        }
    }
}