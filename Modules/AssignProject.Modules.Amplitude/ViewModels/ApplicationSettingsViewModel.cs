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

        private LeadType selectedLead;
        public LeadType SelectedLead { get=> selectedLead; set 
            {
                selectedLead = value;
                RaisePropertyChanged(nameof(SelectedLead));
            } }

        public ObservableCollection<HemisphereType> Hemispheres { get; set; }

        private HemisphereType selectedHemisphere;
        public HemisphereType SelectedHemisphere { get=> selectedHemisphere; set
            {
                selectedHemisphere = value;
                RaisePropertyChanged(nameof(SelectedHemisphere));
            } }

        public ObservableCollection<PortType> PortType { get; set; }

        private PortType selectedPort;
        public PortType SelectedPort { get=> selectedPort; set
            {
                selectedPort = value;
                RaisePropertyChanged(nameof(SelectedPort));
            } }

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
            if(obj !=null)
            {
                if(obj == "Left Brain")
                {
                    SelectedHemisphere = Hemispheres.FirstOrDefault(x => x.Hemisphere == "Left Brain");
                }
                else if (obj == "Right Brain")
                {
                    SelectedHemisphere = Hemispheres.FirstOrDefault(x => x.Hemisphere == "Right Brain");
                }
            }
            //SelectedHemisphere.Hemisphere = obj;
        }

        private void applylead(string obj)
        {
            if (obj != null)
            {
                if (obj == "Lead 1")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 1");
                }
                else if (obj == "Lead 2")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 2");
                }
                else if (obj == "Lead 3")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 3");
                }
                else if (obj == "Lead 4")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 4");
                }
                else if (obj == "Lead 5")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 5");
                }
                else if (obj == "Lead 6")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 6");
                }
                else if (obj == "Lead 7")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 7");
                }
                else if (obj == "Lead 8")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 8");
                }
                else if (obj == "Lead 9")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 9");
                }
                else if (obj == "Lead 10")
                {
                    SelectedLead = LeadType.FirstOrDefault(x => x.Lead == "Lead 10");
                }
            }
            //SelectedLead.Lead = obj;
        }

        private void applyport(string obj)
        {
            if(obj !=null)
            {
                if(obj == "L         ")
                {
                    SelectedPort = PortType.FirstOrDefault(x => x.Port == 'L');
                }
                else if(obj == "R         ")
                    {
                    SelectedPort = PortType.FirstOrDefault(x => x.Port == 'R');
                }
            }
            //SelectedPort.Port = Convert.ToChar(obj);
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
                   
                    
                }
                _eventAggregator.GetEvent<EnableViewButton>().Publish(viewenable);
                _eventAggregator.GetEvent<EnableSettingButtonEvent>().Publish(settingenable);

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
