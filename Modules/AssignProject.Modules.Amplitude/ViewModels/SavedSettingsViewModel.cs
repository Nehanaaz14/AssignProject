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

        private double currentAmp;
        public double CurrentAmp { 
            get=> currentAmp;
            set {
                currentAmp = value;
                RaisePropertyChanged(nameof(CurrentAmp));
            }
        }

        private double targetAmp;
        public double TargetAmp { get=> targetAmp; set { targetAmp = value; RaisePropertyChanged("TargetAmp"); } }

        private string _rampSpeed;
        public string rampSpeed { get=> _rampSpeed; set { _rampSpeed = value; RaisePropertyChanged("rampSpeed"); } }

        private string port;
        public string Port { get=> port; set { port = value; RaisePropertyChanged("Port"); } }

        private string _hemisphere;
        public string hemisphere { get=> _hemisphere; set {
                _hemisphere = value;
                RaisePropertyChanged("hemisphere");
            } }

        private string lead;
        public string Lead { get=>lead; set 
            {
                lead = value;
                RaisePropertyChanged("Lead");
            } }

        public bool viewenable { get; set; }

        public bool settingsenable { get; set; }

        public ICommand CanCelCommand { get; set; }

        public ICommand ApplySettingCommand { get; set; }

        private IRegionManager _regionmanager;

        private int rate;
        public int Rate { get=>rate; set { rate = value; RaisePropertyChanged("Rate"); } }

        private int pulseWidth;
        public int PulseWidth { get=> pulseWidth; set { pulseWidth = value; RaisePropertyChanged("PulseWidth"); } }


         public SavedSettingsViewModel(IRegionManager regionManager, IEventAggregator aggregator)
        {
            this._regionmanager = regionManager;
            this.settings = new ObservableSavedSettingsList(AssignDatabaseData);
            this.CanCelCommand = new DelegateCommand(CancelHandler);
            this.ApplySettingCommand = new DelegateCommand(ApplySettingHandler);
            _aggregator = aggregator;
            TextPropertyChanged();
           DisplaySettings();
        }

        private void ApplySettingHandler()
        {
            viewenable = true;
            settingsenable = true;

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
                _aggregator.GetEvent<EnableSettingButtonEvent>().Publish(settingsenable);
            }
        }

        private void CancelHandler()
        {
            viewenable = true;
            settingsenable = true;
            
            if (this._regionmanager.Regions[RegionNames.SettingsDisplayRegion].NavigationService.Journal.CurrentEntry.Uri.OriginalString
               == "SavedSettings")
            {
                _regionmanager.Regions[RegionNames.SettingsDisplayRegion].NavigationService.RequestNavigate(nameof(AmpPulRate));
                _aggregator.GetEvent<EnableViewButtonEvent>().Publish(viewenable);
                _aggregator.GetEvent<EnableSettingButtonEvent>().Publish(settingsenable);

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

                    TextPropertyChanged();
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

                    TextPropertyChanged();

                }
            }
        }

        private void TextPropertyChanged()
        {
            RaisePropertyChanged(nameof(CurrentAmp));
            RaisePropertyChanged(nameof(TargetAmp));
            RaisePropertyChanged(nameof(rampSpeed));
            RaisePropertyChanged(nameof(Rate));
            RaisePropertyChanged(nameof(PulseWidth));
            RaisePropertyChanged(nameof(Lead));
            RaisePropertyChanged(nameof(Port));
            RaisePropertyChanged(nameof(hemisphere));
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
