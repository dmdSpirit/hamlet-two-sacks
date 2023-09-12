#nullable enable

using UnityEngine;

namespace HamletTwoSacks.Infrastructure
{
    public static class MonoBehaviourUtils
    {
        public static string ToStringID(this MonoBehaviour target, bool getParentName = false)
            => $"{target.GetType().Name} - {(getParentName ? target.transform.parent.name : target.name)}";
    }
}