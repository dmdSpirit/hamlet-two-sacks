#nullable enable

using TMPro;
using UnityEngine;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class FontSizeReactor : SkinReactor
    {
        [SerializeField]
        private TMP_Text _text = null!;

        [SerializeField]
        private float[] _size = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _size.Length)
            {
                Debug.LogError($"Not enough colors for {nameof(ColorSkinReactor)}");
                return;
            }

            float size = _size[skinID];
            if (_text.fontSize != size)
                _text.fontSize = _size[skinID];
        }
    }
}