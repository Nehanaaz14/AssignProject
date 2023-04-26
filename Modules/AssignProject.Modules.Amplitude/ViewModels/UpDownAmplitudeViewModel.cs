using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SharedResource;
using System.Collections.ObjectModel;
using AssignProject.Modules.Amplitude.Models;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class UpDownAmplitudeViewModel : BindableBase 
    {
        public double TargetAmplitude { get; private set; }

        public int AmpIndex;

        protected IDialogService dialogService;

        IEventAggregator _eventAggregator;

        private double totalCurrent;
        public double TotalCurrent
        {
            get => totalCurrent;
            set 
            {
                totalCurrent = value;
                RaisePropertyChanged(nameof(TotalCurrent));
            }
        }

        private static AssignDatabaseDataContext _adpDC = new AssignDatabaseDataContext();
        private readonly ObservableAmplitudeList amplitudeList;

        public int currentchange { get; set; }

        private string rampSpeedItem;
        //private DispatcherTimer rampUpTimer;

        //private Dispatcher Dispatcher { get; }
        public bool enable;

        public double CurrentAmp { get; set; }

        public double TargetAmp { get; set; }

        public float amppointchange { get; set; }

        public double stepInterval { get; set; }

        public ICommand AmplitudeRampDialogCommand { get; set; }

        public DelegateCommand IncrementCurrent { get; private set; }

        public DelegateCommand DecrementCurrent { get; private set; }

        

        public double pointOne = 0.1;

        public UpDownAmplitudeViewModel(IDialogService dialogservice, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.amplitudeList = new ObservableAmplitudeList(_adpDC);
            AmplitudeRampDialogCommand = new DelegateCommand(this.AmpRampDialog);
            this.dialogService = dialogservice;
            _eventAggregator = eventAggregator;
            this.TotalCurrent = 0.0;
            IncrementCurrent = new DelegateCommand(this.IncreaseAmplitude);
            DecrementCurrent = new DelegateCommand(this.DecreaseAmplitude);

            _eventAggregator.GetEvent<ResetAmplitudeValue>().Subscribe(ResetAmplitude);
            _eventAggregator.GetEvent<ApplyCurrentAmplitude>().Subscribe(AmpCurrent);
            _eventAggregator.GetEvent<ApplyTargetAmplitude>().Subscribe(Applytarget);
        }

        private void Applytarget(double obj)
        {
            TargetAmplitude = obj;
            var ind = amplitudeList.Where(x => x.AmplitudeValue == obj).FirstOrDefault();
            if (ind != null)
            {
                AmpIndex = amplitudeList.IndexOf(ind);
            }
            else
            {
                AmpIndex = 0;
            }
        }

        private void AmpCurrent(double obj)
        {
            TotalCurrent = obj;
        }

        private void ResetAmplitude(double amp)
        {
            TotalCurrent = amp;
        }

        private void AmpRampDialog()
        {
            var currentAmp = this.TotalCurrent;

            double current = 0;double target = 0;
            bool temp = false;
            string tempspeed = "";
            int size = 0;

            var parameters = new DialogParameters { { DialogNames.CurrentAmplitudeParameter, currentAmp } ,{ "targerAmpIndex", AmpIndex } };

            this.dialogService.ShowDialog(DialogNames.AmplitudeRampDialog, parameters, am => {
                am.Parameters.TryGetValue<double>(DialogNames.CurrentAmplitudeParameter, out current);
                am.Parameters.TryGetValue<double>("TargetAmplitude", out target);
                am.Parameters.TryGetValue<bool>("DisplayRampUI",out temp);
                am.Parameters.TryGetValue<string>("RampSpeed",out tempspeed);
                //am.Parameters.TryGetValue<int>("targerAmpIndex", AmpIndex);
            });

            CurrentAmp = current;
            TargetAmp = target;
            rampSpeedItem = tempspeed;
            enable = temp;

            _eventAggregator.GetEvent<SaveCurrentAmplitude>().Publish(CurrentAmp);
            _eventAggregator.GetEvent<SaveTargetAmplitude>().Publish(TargetAmp);
            _eventAggregator.GetEvent<SaveRampSpeed>().Publish(rampSpeedItem);
            SetTargetAmplitude(CurrentAmp,TargetAmp,rampSpeedItem);
        }

        private void IncreaseAmplitude()
        {
            TotalCurrent = TotalCurrent + 0.1;

            this.RaisePropertyChanged(nameof(this.TotalCurrent));

        }

        private void DecreaseAmplitude()
        {
            TotalCurrent = TotalCurrent - 0.1;

            if(TotalCurrent < 0) 
            {
                this.TotalCurrent = 0;
            }

            this.RaisePropertyChanged(nameof(this.TotalCurrent));

        }

        private void IncrementtoTargetAmplitude( double amp1, double amp2, double stepInterval, int ch) 
        {
            for(double i = amp1; i <= ch; i++)
            {
                Task.Delay(1000).ContinueWith(_ =>
                {
                    TotalCurrent = TotalCurrent + stepInterval;
                    if ((amp2 - TotalCurrent) < stepInterval)
                    {
                        TotalCurrent = amp2;
                        RaisePropertyChanged(nameof(TotalCurrent));
                    }
                });
               
                    
                    RaisePropertyChanged(nameof(TotalCurrent));
                
            }
        }

        private void DecrementtoTargetAmplitude(double amp1, double amp2, double stepInterval, int ch) 
        {
            for (double i = amp1; i <= ch; i++)
            {
                Task.Delay(1000).ContinueWith(_ =>
                {
                    TotalCurrent = TotalCurrent - stepInterval;
                });
                if ((amp2-TotalCurrent) < stepInterval)
                {
                    TotalCurrent = amp2;
                }
                RaisePropertyChanged(nameof(TotalCurrent));
                
            }
        }

        private void SetTargetAmplitude( double currentAmplitude,double targetAmplitude,  string rampSpeed)
        {
            //// return if the target amplitude is equal to the current amplitude
            if (targetAmplitude == 0 || (targetAmplitude == currentAmplitude))
            {
                RaisePropertyChanged(nameof(TotalCurrent));
                return;
            }

            if(rampSpeed == "Slow (0.1 mA/sec)")
            {
                stepInterval = 0.1;
            }
            else if(rampSpeed == "Medium (0.2 mA/sec)")
            {
                stepInterval = 0.2;
            }
            else if(rampSpeed == "Fast (0.5 mA/sec)")
            {
                stepInterval = 0.5;
            }
            else
            {
                stepInterval = 0;
            }

            if (enable)
            {
                if (CurrentAmp < TargetAmp)
                {
                    currentchange = (int)((TargetAmp - CurrentAmp) * 10);
                    IncrementtoTargetAmplitude(CurrentAmp,TargetAmp,stepInterval, currentchange);
                }
                else if (CurrentAmp > TargetAmp)
                {
                    currentchange = (int)((CurrentAmp - TargetAmp) * 10);
                    DecrementtoTargetAmplitude(CurrentAmp, TargetAmp, stepInterval, currentchange);
                }
            }
        }
    }
}
