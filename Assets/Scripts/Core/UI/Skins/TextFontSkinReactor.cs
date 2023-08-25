#nullable enable
using TMPro;
using UnityEngine;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class TextFontSkinReactor : SkinReactor
    {
        [SerializeField]
        private TMP_Text _target = null!;

        [SerializeField]
        private TMP_FontAsset[] _fonts = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _fonts.Length)
            {
                Debug.LogError($"Not enough colors for {nameof(TextFontSkinReactor)}");
                return;
            }

            TMP_FontAsset font = _fonts[skinID];
            if (_target.font != font)
                _target.font = font;
        }
    }
}