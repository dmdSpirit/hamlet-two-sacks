#nullable enable
using System;

namespace dmdspirit.Core.UI
{
    public interface IUIManager
    {
        IObservable<IUIScreen> OnScreenShown { get; }
        IObservable<IUIScreen> OnScreenHidden { get; }
        void Register(IUIScreen screen);
        T GetScreen<T>() where T : class, IUIScreen;
        void HideAll();
        void Hide<T>() where T : class, IUIScreen;
    }
}