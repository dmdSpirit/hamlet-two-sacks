#nullable enable

using UnityEngine;
using UnityEngine.UI;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class ColorSkinReactor : SkinReactor
    {
        [SerializeField]
        private Image _target = null!;

        [SerializeField]
        private Color[] _colors = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _colors.Length)
            {
                Debug.LogError($"Not enough colors for {nameof(ColorSkinReactor)}");
                return;
            }

            Color color = _colors[skinID];
            if (_target.color != color)
                _target.color = color;
        }
    }
}