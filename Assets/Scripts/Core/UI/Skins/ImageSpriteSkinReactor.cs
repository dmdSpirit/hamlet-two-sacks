#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class ImageSpriteSkinReactor : SkinReactor
    {
        [SerializeField]
        private Image _target = null!;

        [SerializeField]
        private Sprite[] _sprites = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _sprites.Length)
            {
                Debug.LogError($"Not enough sprites for {nameof(ImageSpriteSkinReactor)}");
                return;
            }

            Sprite sprite = _sprites[skinID];
            if (_target.sprite != sprite)
                _target.sprite = sprite;
        }
    }
}