#nullable enable

using System.Collections.Generic;
using dmdspirit.Core.Attributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace dmdspirit.Core.UI.Skins
{
    public class SkinController : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private List<string> _values = new();

        [SerializeField]
        private SkinReactor[] _reactors = null!;

        [SerializeField]
        private int _skinToApply;

#if UNITY_EDITOR
        [SerializeField, Button(nameof(FindSkinReactors))]
        private bool _findSkinReactors;

        [SerializeField, Button(nameof(ApplySkin))]
        private bool _applySkin;
#endif

        public void SetValues(string[] values)
        {
            _values.Clear();
            for (var i = 0; i < values.Length; i++)
                _values.Add($"{i} - {values[i]}");
        }

        public void ActivateSkin(int skinID)
        {
            foreach (SkinReactor skinReactor in _reactors)
                skinReactor.ActivateSkin(skinID);
        }

#if UNITY_EDITOR
        private void FindSkinReactors()
        {
            Undo.RecordObject(this, $"Looking for skin reactors in {name}");
            _reactors = GetComponentsInChildren<SkinReactor>();
        }

        private void ApplySkin()
        {
            Undo.RecordObject(this, $"Activated skin {_skinToApply} for {name}");
            ActivateSkin(_skinToApply);
        }
#endif
    }
}