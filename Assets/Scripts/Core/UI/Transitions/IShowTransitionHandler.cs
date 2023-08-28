#nullable enable
namespace dmdspirit.Core.UI.Transitions
{
    public interface IShowTransitionHandler
    {
        void OnShow();
        void Stop();
    }
}