#nullable enable

namespace HamletTwoSacks.AI.FSMBrain.States
{
    [CreateNodeMenu("Initial Node"), NodeTint("#00ff52")]
    public class InitialStateNode : FSMBaseNode
    {
        [Output]
        public StateNode? InitialNode;

        public StateNode? NextNode => InitialNode != null ? GetFirst<StateNode>(nameof(InitialNode)) : null;
    }
}