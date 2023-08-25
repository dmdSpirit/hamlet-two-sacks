#nullable enable
using TMPro;
using UnityEngine;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class TextColorSkinReactor : SkinReactor
    {
        [SerializeField]
        private TMP_Text _target = null!;

        [SerializeField]
        private Color[] _colors = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _colors.Length)
            {
                Debug.LogError($"Not enough colors for {nameof(TextColorSkinReactor)}");
                return;
            }

            Color color = _colors[skinID];
            if (_target.color != color)
                _target.color = color;
        }
    }
}