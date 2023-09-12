#nullable enable

using System.Linq;
using UnityEngine;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class SystemReferences : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour[]? _systems;

        public T? GetSystem<T>() where T : MonoBehaviour
            => (T?)_systems?.FirstOrDefault(system => system is T);
    }
}