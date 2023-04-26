using AssignProject.Modules.Amplitude.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SharedResource;
using System;
using System.Collections.Generic;
using System.Linq;
using AssignProject.Modules.Amplitude.Models;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CommonServiceLocator;
using Prism.Events;
using System.Windows;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class ApplicationSettingsViewModel : BindableBase, INavigationAware
    {
        private ObservableSavedSettingsList settings;
        public ObservableCollection<LeadType> LeadType { get; set; }

        AssignDatabaseDataContext assign = new AssignDatabaseDataContext();

        public LeadType SelectedLead { get; set; }

        public ObservableCollection<HemisphereType> Hemispheres { get; set; }

        public HemisphereType SelectedHemisphere { get; set; }

        public ObservableCollection<PortType> PortType { get; set; }

        public PortType SelectedPort { get; set; }

        IEventAggregator _eventAggregator;

        private readonly IRegionManager _regionmanager;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand BackCommand { get; set; }

        public DelegateCommand ClearDatabaseCommand { get; set; }
        public bool settingenable { get; private set; }
        public bool viewenable { get; private set; }

        public ApplicationSettingsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this._regionmanager = regionManager;
            _eventAggregator = eventAggregator;
            BackCommand = new DelegateCommand(PreviousViewHandler);
            ClearDatabaseCommand = new DelegateCommand(ClearDatabaseHandler);
            LeadType = new ObservableCollection<LeadType>()
            {
                new LeadType{Lead="Lead 1"},
                new LeadType{Lead="Lead 2"},
                new LeadType{Lead="Lead 3"},
                new LeadType{Lead="Lead 4"},
                new LeadType{Lead="Lead 5"},
                new LeadType{Lead="Lead 6"},
                new LeadType{Lead="Lead 7"},
                new LeadType{Lead="Lead 8"},
                new LeadType{Lead="Lead 9"},
                new LeadType{Lead="Lead 10"}

            };

            PortType = new ObservableCollection<PortType>() 
            {
                new PortType{Port = 'L'},
                new PortType{Port = 'R'}
            };

            settings = new ObservableSavedSettingsList(assign);

            Hemispheres = new ObservableCollection<HemisphereType>()
            {
                new HemisphereType{Hemisphere="Left Brain"},
                new HemisphereType{Hemisphere="Right Brain"}
            };

            _eventAggregator.GetEvent<ApplyPort>().Subscribe(applyport);
            _eventAggregator.GetEvent<ApplyLead>().Subscribe(applylead);
            _eventAggregator.GetEvent<ApplyHemisphere>().Subscribe(applyhemisphere);
        }

        private void applyhemisphere(string obj)
        {
            SelectedHemisphere.Hemisphere = obj;
        }

        private void applylead(string obj)
        {
            SelectedLead.Lead = obj;
        }

        private void applyport(string obj)
        {
            SelectedPort.Port = Convert.ToChar( obj);
        }

        private void ClearDatabaseHandler()
        {
            using (assign)
            {
                if(assign.SavedSettings.Count() == 0)
                {
                    MessageBox.Show("No Records To Clear in Database", "Empty Database! ");
                }
                else
                {
                    assign.ExecuteCommand("DELETE FROM SavedSettings");
                    assign.SubmitChanges();
                    MessageBox.Show("Database Cleared Successfully", "Database Status");
                }
                
            }  
        }

        private void PreviousViewHandler()
        {
            settingenable = true;
            viewenable = true;

            if (this._regionmanager.Regions[RegionNames.SettingsDisplayRegion].NavigationService.Journal.CurrentEntry.Uri.OriginalString
                == "ApplicationSettings")
            {

                _regionmanager.Regions[RegionNames.SettingsDisplayRegion].NavigationService.RequestNavigate(nameof(AmpPulRate));
                if(SelectedHemisphere != null || SelectedLead !=null || SelectedPort != null)
                {
                    _eventAggregator.GetEvent<Savehemisphere>().Publish(SelectedHemisphere.Hemisphere);
                    _eventAggregator.GetEvent<SaveLead>().Publish(SelectedLead.Lead);
                    _eventAggregator.GetEvent<SavePort>().Publish(SelectedPort.Port.ToString());
                    _eventAggregator.GetEvent<EnableSettingButtonEvent>().Publish(settingenable);
                    
                }
                _eventAggregator.GetEvent<EnableViewButton>().Publish(viewenable);

            }

            

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
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
