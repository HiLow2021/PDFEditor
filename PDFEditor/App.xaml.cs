using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using PDFEditor.Models;
using PDFEditor.Views;

namespace PDFEditor
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : PrismApplication
    {
        public MyAppSettings Settings { get; private set; }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnInitialized()
        {
            var regionManager = Container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion("MainWindowContentRegion", typeof(PreviewView));
            regionManager.RequestNavigate("MainWindowContentRegion", nameof(PreviewView));

            MainWindow.Loaded += MainWindow_Loaded;
            MainWindow.Closing += MainWindow_Closing;

            base.OnInitialized();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Settings = new MyAppSettings();

            containerRegistry.RegisterInstance(Settings);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(MyAppSettings.WorkDirectory))
            {
                Directory.CreateDirectory(MyAppSettings.WorkDirectory);
            }
        }

        private void MainWindow_Closing(object sender, EventArgs e)
        {
            
        }
    }
}
