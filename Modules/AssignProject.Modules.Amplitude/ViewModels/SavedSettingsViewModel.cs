using AssignProject.Modules.Amplitude.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SharedResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class SavedSettingsViewModel : BindableBase , INavigationAware
    {
        private AssignDatabaseDataContext AssignDatabaseData = new AssignDatabaseDataContext();
        private ObservableSavedSettingsList settings;

        public IEventAggregator _aggregator;

        public double CurrentAmp { get; set; }

        public double TargetAmp { get; set; }

        public string rampSpeed { get; set; }

        public string Port { get; set; }

        public string hemisphere { get; set; }

        public string Lead { get; set; }

        public bool viewenable { get; set; }

        public ICommand CanCelCommand { get; set; }

        public ICommand ApplySettingCommand { get; set; }

        private IRegionManager _regionmanager;

        public int Rate { get; set; }
        public int PulseWidth { get; set; }
         public SavedSettingsViewModel(IRegionManager regionManager, IEventAggregator aggregator)
        {
            this._regionmanager = regionManager;
            this.settings = new ObservableSavedSettingsList(AssignDatabaseData);
            this.CanCelCommand = new DelegateCommand(CancelHandler);
            this.ApplySettingCommand = new DelegateCommand(ApplySettingHandler);
            _aggregator = aggregator;
            DisplaySettings();
        }

        private void ApplySettingHandler()
        {
            viewenable = true;
            if (this._regionmanager.Regions[RegionNames.SettingsDisplayRegion].NavigationService.Journal.CurrentEntry.Uri.OriginalString
              == "SavedSettings")
            {
                _regionmanager.Regions[RegionNames.SettingsDisplayRegion].NavigationService.RequestNavigate(nameof(AmpPulRate));
                _aggregator.GetEvent<ApplyCurrentAmplitude>().Publish(CurrentAmp);
                _aggregator.GetEvent<ApplyTargetAmplitude>().Publish(TargetAmp);
                _aggregator.GetEvent<ApplyRampSpeed>().Publish(rampSpeed) ;
                _aggregator.GetEvent<ApplyRate>().Publish(Rate);
                _aggregator.GetEvent<ApplyPulse>().Publish(PulseWidth);
                _aggregator.GetEvent<ApplyLead>().Publish(Lead);
                _aggregator.GetEvent<ApplyPort>().Publish(Port);
                _aggregator.GetEvent<ApplyHemisphere>().Publish(hemisphere);
                _aggregator.GetEvent<EnableViewButtonEvent>().Publish(viewenable);
            }
        }

        private void CancelHandler()
        {
            viewenable = true;
            
            if (this._regionmanager.Regions[RegionNames.SettingsDisplayRegion].NavigationService.Journal.CurrentEntry.Uri.OriginalString
               == "SavedSettings")
            {
                _regionmanager.Regions[RegionNames.SettingsDisplayRegion].NavigationService.RequestNavigate(nameof(AmpPulRate));
                _aggregator.GetEvent<EnableViewButtonEvent>().Publish(viewenable);

            }
           
        }

        private void DisplaySettings()
        {
            SavedSetting setting = new SavedSetting();
           
            //CurrentAmp = AssignDatabaseData.SavedSettings.Select(c=>c.MinAmplitude.Value).Last();
            using (AssignDatabaseData)
            {
                if(AssignDatabaseData.SavedSettings.Count() == 0)
                {
                    MessageBox.Show("No Records in Database", "Empty Database!", MessageBoxButton.OK);
                }
                else
                {
                    var lastelement = AssignDatabaseData.SavedSettings.OrderByDescending(i => i.ID).Select(b => new
                    {
                        CurrentAmp = b.MinAmplitude,
                        TargetAmp = b.MaxAmplitude,
                        rampSpeed = b.RampSpeed,
                        Rate = b.Rate,
                        PulseWidth = b.Pulse_Width,
                        Lead = b.SLeadType,
                        Port = b.SPortType,
                        hemisphere = b.SHemisphere
                    }).FirstOrDefault();

                    CurrentAmp = (double)lastelement.CurrentAmp;
                    TargetAmp = (double)lastelement.TargetAmp;
                    rampSpeed = lastelement.rampSpeed;
                    Rate = (int)lastelement.Rate;
                    PulseWidth = (int)lastelement.PulseWidth;
                    Lead = lastelement.Lead;
                    Port = lastelement.Port;
                    hemisphere = lastelement.hemisphere;
                    
                }
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }
    }
}
