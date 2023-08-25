#nullable enable

using UnityEngine;

namespace dmdspirit.Core.UI
{
    public class SimpleHideObjectHandler : MonoBehaviour, IHideTransitionHandler
    {
        [SerializeField]
        protected bool _log;

        public void OnHide()
        {
            if (gameObject == null)
                return;

            if (_log)
                Debug.Log($"Hiding {gameObject.name}");

            HideInternal();
        }

        public virtual void Stop()
        {
            if (_log)
                Debug.Log($"{gameObject.name}: Hide handler stopped");
        }

        protected virtual void HideInternal()
            => gameObject.SetActive(false);
    }
}