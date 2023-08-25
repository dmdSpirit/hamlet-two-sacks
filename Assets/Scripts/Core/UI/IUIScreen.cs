#nullable enable
using System;
using Zenject;

namespace dmdspirit.Core.UI
{
    public interface IUIScreen : IInitializable
    {
        public bool IsShown { get; }
        public bool IsInitialized { get; }
        public void Show();
        public void Hide();
        public IObservable<IUIScreen> OnScreenShown { get; }
        public IObservable<IUIScreen> OnScreenHidden { get; }
    }
}