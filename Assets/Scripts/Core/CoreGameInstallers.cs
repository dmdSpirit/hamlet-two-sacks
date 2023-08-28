#nullable enable

using dmdspirit.Core.AssetManagement;
using dmdspirit.Core.UI;
using Zenject;

namespace dmdspirit.Core
{
    public sealed class CoreGameInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIManager>().To<UIManager>().AsSingle().NonLazy();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle().NonLazy();
            Container.Bind<IGameQuitter>().To<GameQuitter>().AsSingle();
        }
    }
}