#nullable enable
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace dmdspirit.Core.UI
{
    public sealed class UpdatableProgressBar : MonoBehaviour
    {
        private IDisposable? _sub;
        
        [SerializeField]
        private Image _progress = null!;

        public void StartShowing(IReadOnlyReactiveProperty<float> progress)
        {
            gameObject.SetActive(true);
            _sub?.Dispose();
            _sub = progress.Subscribe(OnUpdate);
        }
        
        public void StopShowing()
        {
            _sub?.Dispose();
            gameObject.SetActive(false);
        }

        private void OnUpdate(float progress)
            => _progress.fillAmount = Mathf.Clamp(progress, 0, 1);
    }
}