#nullable enable

using dmdspirit.Core.AssetManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.AssetManagement
{
    public sealed class SpriteLoader : MonoBehaviour
    {
        private IAssetProvider _assetProvider = null!;

        [SerializeField]
        private Image _image = null!;

        [Inject]
        private void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async void LoadSprite(AssetReferenceT<Sprite> assetReference)
        {
            var sprite = await _assetProvider.Load<Sprite>(assetReference);
            _image.sprite = sprite;
        }
    }
}