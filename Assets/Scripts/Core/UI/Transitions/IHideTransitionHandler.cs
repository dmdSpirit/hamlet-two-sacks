#nullable enable

namespace dmdspirit.Core.UI.Transitions
{
    public interface IHideTransitionHandler
    {
        void OnHide();
        void Stop();
    }
}