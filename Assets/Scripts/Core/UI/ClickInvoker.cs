#nullable enable

using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace dmdspirit.Core.UI
{
    public sealed class ClickInvoker : MonoBehaviour, IPointerClickHandler
    {
        private readonly Subject<Unit> _onClick = new();

        public IObservable<Unit> OnClick => _onClick;

        public void OnPointerClick(PointerEventData eventData)
            => _onClick.OnNext(Unit.Default);
    }
}