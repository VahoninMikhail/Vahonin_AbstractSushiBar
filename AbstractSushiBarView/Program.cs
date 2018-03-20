using AbstractSushiBarService.ImplementationsList;
using AbstractSushiBarService.Interfaces;
using System;
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
            currentContainer.RegisterType<IVisitorService, VisitorServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngredientService, IngredientServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICookService, CookServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISushiService, SushiServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IBaseService, BaseServiceList>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
