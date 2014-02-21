using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DTS.Framework.CodeGen;
using DTS.Framework.Designer.Services;
using DTS.Framework.Designer.ViewModels;
using Microsoft.Practices.Unity;

namespace DTS.Framework.Designer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BootstrapUnity();

            MainWindow mainWindow = new MainWindow(UnityContainer.Resolve<MainViewModel>());
            
            mainWindow.Show();
        }

        private void BootstrapUnity()
        {
            UnityContainer = new UnityContainer();

            UnityContainer.RegisterType<Generator>(new ContainerControlledLifetimeManager());
            UnityContainer.RegisterType<CodeGenService>(new ContainerControlledLifetimeManager());

            UnityContainer.RegisterType<MainViewModel>(new InjectionConstructor(typeof (CodeGenService), typeof (DomainDefinitionService)));
        }

        public static UnityContainer UnityContainer { get; set; }
    }
}
