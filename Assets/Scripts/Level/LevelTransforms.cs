#nullable enable

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HamletTwoSacks.Level
{
    public sealed class LevelTransforms : MonoBehaviour
    {
        // TODO (Stas): Decide what to do with this
        // - Stas 14 September 2023
        private Dictionary<Type, Transform> _transforms = new();

        [SerializeField]
        private Transform _units = null!;

        [SerializeField]
        private Transform _crystals = null!;

        public Transform Units => _units;
        public Transform Crystals => _crystals;

        public Transform GetParent<T>() where T : MonoBehaviour
        {
            if (_transforms.TryGetValue(typeof(T), out Transform parent))
                return parent;
            return transform;
        }
    }
}