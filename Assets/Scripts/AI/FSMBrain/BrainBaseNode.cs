#nullable enable
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace HamletTwoSacks.AI.FSMBrain
{
    public abstract class BrainBaseNode : Node
    {
        [Input(ShowBackingValue.Never), SerializeField]
        private BrainBaseNode _entry;

        protected T? GetFirst<T>(string fieldName) where T : BrainBaseNode
        {
            NodePort? port = GetOutputPort(fieldName);
            if (port.ConnectionCount == 0)
                return null;
            return port.GetConnection(0).node as T;
        }

        protected IEnumerable<T> GetAllOnPort<T>(string fieldName) where T : BrainBaseNode
        {
            NodePort? port = GetOutputPort(fieldName);
            for (var i = 0; i < port.ConnectionCount; i++)
                yield return (port.GetConnection(i).node as T)!;
        }

        public override object GetValue(NodePort port)
            => port.ConnectionCount > 0 ? port.GetConnection(0).node : null!;
    }
}