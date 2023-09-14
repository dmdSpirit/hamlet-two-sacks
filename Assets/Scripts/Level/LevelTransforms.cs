#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

namespace HamletTwoSacks.Level
{
    public sealed class LevelTransforms : MonoBehaviour
    {
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