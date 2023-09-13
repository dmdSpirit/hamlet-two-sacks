#nullable enable

using System.Linq;
using UnityEngine;

namespace HamletTwoSacks.Infrastructure.StaticData
{
    // FIXME (Stas): Will this load every prefab into memory?
    // - Stas 13 September 2023
    [CreateAssetMenu(menuName = "Config/Prefabs Config", fileName = "prefabs_config", order = 0)]
    public sealed class PrefabsConfig : GameConfig
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