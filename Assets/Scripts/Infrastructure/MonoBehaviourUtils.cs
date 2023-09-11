#nullable enable
using UnityEngine;

namespace HamletTwoSacks.Infrastructure
{
    public static class MonoBehaviourUtils
    {
        public static string ToStringID(this MonoBehaviour target)
            => $"{target.GetType().Name} - {target.name}";
    }
}