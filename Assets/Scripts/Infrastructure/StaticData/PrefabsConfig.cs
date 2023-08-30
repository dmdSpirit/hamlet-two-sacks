#nullable enable

using System.Linq;
using UnityEngine;

namespace HamletTwoSacks.Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Config/Prefabs Config", fileName = "prefabs_config", order = 0)]
    public sealed class PrefabsConfig : ScriptableObject
    {
        [SerializeField]
        private MonoBehaviour[] _prefabs = null!;

        public T GetPrefab<T>() where T : MonoBehaviour
        {
            MonoBehaviour? prefab = _prefabs.FirstOrDefault(p => p is T);
            if (prefab != null)
                return (T)prefab;
            Debug.LogError($"Could not find prefab of type {typeof(T).Name} in {name}");
            return null!;
        }
    }
}