#nullable enable
using UniRx;

namespace dmdspirit.Core.CommonInterfaces
{
    public interface IActivatableReactive
    {
        void Activate();
        void Deactivate();
        IReadOnlyReactiveProperty<bool> IsActive { get; }
    }
}