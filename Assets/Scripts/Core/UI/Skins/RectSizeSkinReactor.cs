#nullable enable
using UnityEngine;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class RectSizeSkinReactor : SkinReactor
    {
        [SerializeField]
        private RectTransform _target = null!;

        [SerializeField]
        private Vector2[] _size = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _size.Length)
            {
                Debug.LogError($"Not enough reactor values for {nameof(RectSizeSkinReactor)}");
                return;
            }

            Vector2 size = _size[skinID];
            if (_target.sizeDelta != size)
                _target.sizeDelta = size;
        }
    }
}