#nullable enable

using System.Linq;
using HamletTwoSacks.AI.FSMBrain.States;
using UnityEngine;
using XNode;

namespace HamletTwoSacks.AI.FSMBrain
{
    [CreateAssetMenu(menuName = "FSM/Brain Graph")]
    public class BrainGraph : NodeGraph
    {
        private InitialStateNode? _initialStateNode;

        public StateNode? InitialState => GetInitialState();

        private StateNode? GetInitialState()
        {
            if (_initialStateNode != null)
                return _initialStateNode.GetNextNode();
            _initialStateNode = (InitialStateNode?)nodes.FirstOrDefault(node => node is InitialStateNode);
            if (_initialStateNode != null)
                return _initialStateNode.GetNextNode();
            Debug.LogError($"FSM Graph {name} could not find initial state.");
            return null;
        }
    }
}