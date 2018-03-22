using AbstractSushiBarService;
using AbstractSushiBarService.ImplementationsBD;
using AbstractSushiBarService.ImplementationsList;
using AbstractSushiBarService.Interfaces;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractSushiBarView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormBase>());
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

            return currentContainer;
        }
    }
}
