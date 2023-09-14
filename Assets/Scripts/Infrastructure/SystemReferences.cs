#nullable enable

using System.Linq;
using UnityEngine;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class SystemReferences : MonoBehaviour
    {
        [SerializeField]
        private Component[]? _systems;

        public T? GetSystem<T>() where T : Component
            => (T?)_systems?.FirstOrDefault(system => system is T);

        public T GetSystemWithCheck<T>() where T : Component
        {
            var system = (T?)_systems?.FirstOrDefault(system => system is T);
            if (system == null)
            {
                Debug.LogError($"Could not get system {typeof(T).Name} for {name}");
                return null!;
            }

            return system;
        }
    }
}