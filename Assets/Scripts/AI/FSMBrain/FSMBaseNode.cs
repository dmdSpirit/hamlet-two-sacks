#nullable enable
using System.Collections.Generic;
using XNode;

namespace HamletTwoSacks.AI.FSMBrain
{
    public class FSMBaseNode : Node
    {
        // whats this?
        // [Input(ShowBackingValue.Never)]
        public FSMBaseNode Entry;

        protected T? GetFirst<T>(string fieldName) where T : FSMBaseNode
        {
            NodePort? port = GetOutputPort(fieldName);
            if (port.ConnectionCount == 0)
                return null;
            return port.GetConnection(0).node as T;
        }

        protected IEnumerable<T> GetAllOnPort<T>(string fieldName) where T : FSMBaseNode
        {
            NodePort? port = GetOutputPort(fieldName);
            for (var i = 0; i < port.ConnectionCount; i++)
                yield return (port.GetConnection(i).node as T)!;
        }
    }
}