using AssignProject.Core.Regions;
using AssignProject.Modules.Amplitude;
using AssignProject.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

namespace AssignProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
           moduleCatalog.AddModule<AmplitudeModule>();
            //base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdaptor>());
        }

        protected override void OnInitialized()
        {
            var startapp = Container.Resolve<StartUpWindow>();
            var result = startapp.ShowDialog();

            if (result.Value)
                base.OnInitialized();
            else
                Application.Current.Shutdown();


        }
    }
}
