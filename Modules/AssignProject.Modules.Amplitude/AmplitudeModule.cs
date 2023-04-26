using AssignProject.Modules.Amplitude.ViewModels;
using AssignProject.Modules.Amplitude.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using SharedResource;

namespace AssignProject.Modules.Amplitude
{
    public class AmplitudeModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public AmplitudeModule(IRegionManager regionManager) //constructor with IRegionManager parameter
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {

            _regionManager.RegisterViewWithRegion(RegionNames.AmplitudeSetRegion, typeof(UpDownAmplitude));
            _regionManager.RegisterViewWithRegion(RegionNames.SettingsButtonSetRegion, typeof(SettingsButton));
            _regionManager.RegisterViewWithRegion(RegionNames.PulseWidthSetRegion, typeof(PulseWidthRate));
            _regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(MainHeader));
            _regionManager.RegisterViewWithRegion(RegionNames.SettingsDisplayRegion, typeof(AmpPulRate));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Register View and ViewModel
            //ViewModelLocationProvider.Register<UpDownAmplitude, UpDownAmplitudeViewModel>();
            //ViewModelLocationProvider.Register<PulseWidthRate, PulseWidthRateViewModel>();
            ViewModelLocationProvider.Register<MainHeader,MainHeaderViewModel>();
            //ViewModelLocationProvider.Register<AmpPulRate, AmpPulRateViewModel>();

            containerRegistry.RegisterForNavigation<ApplicationSettings, ApplicationSettingsViewModel>();
            containerRegistry.RegisterForNavigation<SavedSettings, SavedSettingsViewModel>();
            containerRegistry.RegisterForNavigation<AmpPulRate, AmpPulRateViewModel>();
            containerRegistry.RegisterForNavigation<UpDownAmplitude, UpDownAmplitudeViewModel>();
            containerRegistry.RegisterForNavigation<PulseWidthRate, PulseWidthRateViewModel>();

            containerRegistry.RegisterDialog<RateSelectView, RateSelectViewModel>(DialogNames.RateSelectDialog);
            containerRegistry.RegisterDialog<ViewA, ViewAViewModel>(DialogNames.AmplitudeRampDialog);
            containerRegistry.RegisterDialog<PulseWidthOption, PulseWidthOptionViewModel>(DialogNames.PulseWidthOptionDialog);
            containerRegistry.RegisterDialog<RateSetting, RateSettingViewModel>(DialogNames.RateSetting);
        }
    }
}