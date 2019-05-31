using Microsoft.EntityFrameworkCore;
using ModuleDataBase.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleDataBase
{
    public class ModuleDataBaseModule : DbContext, IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}