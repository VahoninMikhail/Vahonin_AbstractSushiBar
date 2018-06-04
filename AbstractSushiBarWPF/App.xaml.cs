using AbstractSushiBarService;
using AbstractSushiBarService.ImplementationsBD;
using AbstractSushiBarService.Interfaces;
using System;
using System.Data.Entity;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace AbstractSushiBarWPF
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var container = BuildUnityContainer();

            var application = new App();
            application.Run(container.Resolve<BaseWindow>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IVisitorService, VisitorServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngredientService, IngredientServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICookService, CookServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISushiService, SushiServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IBaseService, BaseServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportServiceBD>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
