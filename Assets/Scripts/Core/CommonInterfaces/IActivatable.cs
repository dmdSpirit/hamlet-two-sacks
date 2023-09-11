#nullable enable
namespace dmdspirit.Core.CommonInterfaces
{
    public interface IActivatable
    {
        void Activate();
        void Deactivate();
        bool IsActive { get; }
    }
}