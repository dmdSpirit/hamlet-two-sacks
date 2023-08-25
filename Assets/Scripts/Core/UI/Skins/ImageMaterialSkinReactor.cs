#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class ImageMaterialSkinReactor : SkinReactor
    {
        [SerializeField]
        private Image _target = null!;

        [SerializeField]
        private Material[] _materials = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _materials.Length)
            {
                Debug.LogError($"Not enough materials for {nameof(ImageMaterialSkinReactor)}");
                return;
            }

            Material material = _materials[skinID];
            if (_target.material != material)
                _target.material = material;
        }
    }
}