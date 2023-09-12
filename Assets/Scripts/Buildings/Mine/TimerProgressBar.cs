#nullable enable

using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HamletTwoSacks.Buildings.Mine
{
    public sealed class TimerProgressBar : MonoBehaviour
    {
        private IDisposable? _sub;
        private ProgressTimer? _timer;

        [SerializeField]
        private Image _progress = null!;

        [SerializeField]
        private bool _hideWithoutProgress;

        private void OnDestroy()
            => _sub?.Dispose();

        public void SetTimer(ProgressTimer timer)
        {
            _sub?.Dispose();
            _timer = timer;
            _sub = _timer.Progress.Subscribe(OnUpdate);
            if (_hideWithoutProgress && !_timer.HadAnyProgress)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }

        private void OnUpdate(float progress)
        {
            _progress.fillAmount = progress;
            if (!gameObject.activeInHierarchy
                && _timer!.HadAnyProgress)
                gameObject.SetActive(true);
        }
    }
}