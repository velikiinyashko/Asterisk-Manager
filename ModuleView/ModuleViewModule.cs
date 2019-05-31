using ModuleView.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleView
{
    public class ModuleViewModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var RegionManager = containerProvider.Resolve<IRegionManager>();

            RegionManager.RequestNavigate("MainView", "MainWindows");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindows>("MainWindows");
        }
    }
}