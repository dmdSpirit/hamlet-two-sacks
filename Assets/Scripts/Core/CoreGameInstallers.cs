#nullable enable
using dmdspirit.Core.UI;
using Zenject;

namespace dmdspirit.Core
{
    public sealed class CoreGameInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UIManager>().AsSingle();
        }
    }
}