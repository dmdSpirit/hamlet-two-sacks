#nullable enable
using UnityEngine;

namespace dmdspirit.Core.UI.Transitions
{
    public class SimpleShowObjectHandler : MonoBehaviour, IShowTransitionHandler
    {
        [SerializeField]
        protected bool _log;

        public void OnShow()
        {
            if (gameObject == null)
                return;

            if (_log)
                Debug.Log($"Showing {gameObject.name}");

            ShowInternal();
        }

        public virtual void Stop()
        {
            if (_log)
                Debug.Log($"{gameObject.name}: Show handler stopped");
        }

        protected virtual void ShowInternal()
            => gameObject.SetActive(true);
    }
}