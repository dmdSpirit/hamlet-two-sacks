#nullable enable
using UnityEngine;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class GameObjectActiveReactor : SkinReactor
    {
        [SerializeField]
        private GameObject _target = null!;

        [SerializeField]
        private bool[] _isActive = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _isActive.Length)
            {
                Debug.LogError($"Not enough reactor values for {nameof(GameObjectActiveReactor)}");
                return;
            }

            bool isActive = _isActive[skinID];
            if (_target.activeInHierarchy != isActive)
                _target.SetActive(isActive);
        }
    }
}