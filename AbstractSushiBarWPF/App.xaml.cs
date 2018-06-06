using System;
using System.Windows;

namespace AbstractSushiBarWPF
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            APIClient.Connect();
            var application = new App();
            application.Run(new BaseWindow());
        }
    }
}
