#nullable enable

using UnityEngine;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class RectScaleSkinReactor : SkinReactor
    {
        [SerializeField]
        private RectTransform _target = null!;

        [SerializeField]
        private Vector3[] _scales = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _scales.Length)
            {
                Debug.LogError($"Not enough colors for {nameof(RectScaleSkinReactor)}");
                return;
            }

            Vector3 scale = _scales[skinID];
            if (_target.localScale != scale)
                _target.localScale = scale;
        }
    }
}