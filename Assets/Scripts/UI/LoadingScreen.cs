#nullable enable
using dmdspirit.Core.UI;
using UnityEngine;

namespace HamletTwoSacks.UI
{
    public sealed class LoadingScreen : UIScreen
    {
        [SerializeField]
        private RectTransform _progress = null!;

        protected override void OnInitialize()
        {
            gameObject.SetActive(false);
        }

        protected override void OnShow() { }

        protected override void OnHide() { }

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp(progress, 0f, 1f);
            Vector3 scale = _progress.localScale;
            scale.x = progress;
            _progress.localScale = scale;
        }
    }
}