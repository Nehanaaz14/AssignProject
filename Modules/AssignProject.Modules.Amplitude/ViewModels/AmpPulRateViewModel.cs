using AssignProject.Modules.Amplitude.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SharedResource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class AmpPulRateViewModel : BindableBase , INavigationAware
    {
        //private ObservableCollection<PulseWidthRateViewModel> pulseWidthRates;

        //private ObservableCollection<UpDownAmplitudeViewModel> upDownAmplitudes;

        //private ObservableCollection<SettingsButtonViewModel> settingsButtons;

        private readonly IRegionManager regionmanager;

        public int navigationCounter;

        public AmpPulRateViewModel(IRegionManager RegionManager)
        {
            this.navigationCounter = 0;
            this.regionmanager = RegionManager;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        { 
  
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
           // regionmanager.Regions[RegionNames.SettingsDisplayRegion].NavigationService.RequestNavigate("ApplicationSettings");
        }
    }
}
