using AbstractSushiBarService.ImplementationsList;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarWPF;
using System;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace WpfMotorZavod
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
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
