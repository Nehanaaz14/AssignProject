using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResource;
using System.Windows.Input;
using System.Globalization;
using System.Collections.ObjectModel;
using AssignProject.Modules.Amplitude.Models;
using Prism.Events;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class ViewAViewModel : BindableBase , IDialogAware
    {
        private string _messageText;

        private const int DefaultStepSize = 10;

        private const float DefaultStepInterval = 1.0f;

        private int selectedAmplitudeItemIndex;
        public int SelectedAmplitudeItemIndex {
            get => this.selectedAmplitudeItemIndex;
            set
            {
                this.selectedAmplitudeItemIndex = value;
                this.RaisePropertyChanged(nameof(selectedAmplitudeItemIndex));
            } }

        private static AssignDatabaseDataContext _adpDC = new AssignDatabaseDataContext();
        private readonly ObservableAmplitudeList amplitudeList;

        private RampSpeedModel rampSpeedItem;

        public bool IsDisplayMessage => this.MessageText.Length > 0;

        private int stepSize;

        private float stepInterval;
        public ObservableCollection<RampSpeedModel> RampSpeedItemList { get; }

        private RampSpeedModel defaultRampSpeed;

        public RampSpeedModel SelectedRampSpeed
        {
            get => this.rampSpeedItem;
            set
            {
                this.rampSpeedItem = value;
                this.UpdateRampSpeed();
                this.IsSetRampSpeedAsDefault = false;
                this.RaisePropertyChanged(nameof(this.SelectedRampSpeed));
                this.RaisePropertyChanged(nameof(this.MessageText));
                this.RaisePropertyChanged(nameof(this.IsDisplayMessage));
                //this.RaisePropertyChanged(nameof(this.IsEnableSelectedRampSpeedAsDefault));
            }
        }

        private double currentAmplitude;
        public double CurrentAmplitude
        {
            get => this.currentAmplitude;
            set
            {
                this.currentAmplitude = value;
                this.RaisePropertyChanged(nameof(this.CurrentAmplitude));
            }
        }

        private double amplitudeValue;
        public double AmplitudeValue
        {
            get => this.amplitudeValue;
            set 
            {
                amplitudeValue = value;
                this.RaisePropertyChanged(nameof(this.AmplitudeValue));
            }
        }

        private double targetAmplitude;

        public double TargetAmplitude
        {
            get => this.targetAmplitude;

            set
            {
                this.targetAmplitude = value;
                this.RaisePropertyChanged(nameof(this.CanExecuteOKCommand));
               // If current area amplitude is greater than target amplitude set the step interval as "OFF"
                //if (this.CurrentAmplitude > this.TargetAmplitude)
                //{
                //    this.SelectedRampSpeed = this.RampSpeedItemList.FirstOrDefault(x => x.RampSpeedItemName == RampSpeedItem.Instant);
                //}

                // If current area amplitude is less than target amplitude set the default step interval i.e. 1
                //if (this.CurrentAmplitude < this.TargetAmplitude && this.SelectedRampSpeed.RampSpeedItemName == RampSpeedItem.Instant)
                //{
                //    this.SelectedRampSpeed = this.defaultRampSpeed;
                //    this.IsDefaultStepInterval = true;
                //}

                this.RaisePropertyChanged(nameof(this.TargetAmplitude));
                this.RaisePropertyChanged(nameof(this.MessageText));
                this.RaisePropertyChanged(nameof(this.IsDisplayMessage));
                this.RaisePropertyChanged(nameof(this.IsTargetAmplitudeSelected));
            }
        }

        private bool isSetRampSpeedAsDefault;
        public bool IsSetRampSpeedAsDefault
        {
            get => this.isSetRampSpeedAsDefault;
            set
            {
                this.isSetRampSpeedAsDefault = value;
                this.RaisePropertyChanged(nameof(this.IsSetRampSpeedAsDefault));
                this.RaisePropertyChanged(nameof(this.IsDisplayMessage));
                this.RaisePropertyChanged(nameof(this.MessageText));
            }
        }

        private bool isDefaultStepInterval;
        public bool IsDefaultStepInterval
        {
            get => this.isDefaultStepInterval;

            set
            {
                this.isDefaultStepInterval = value;

                // If default interval is selected set step interval as 1
                if (this.isDefaultStepInterval)
                {
                    this.stepInterval = DefaultStepInterval;
                }

                this.RaisePropertyChanged(nameof(this.IsDefaultStepInterval));
            }
        }

        public string PointOneStep { get; set; }

        public string PointFiveStep { get; set; }

        public bool IsTargetAmplitudeSelected => this.TargetAmplitude != null;
        private bool CanExecuteOKCommand => this.TargetAmplitude != null;

        public event Action<IDialogResult> RequestClose;

        public string MessageText
        {
            get 
            {
                if (this.TargetAmplitude != null && this.CurrentAmplitude < this.TargetAmplitude && this.SelectedRampSpeed?.RampSpeedItemName == RampSpeedItem.Instant)
                {
                    return string.Format(
                        CultureInfo.CurrentCulture,
                        "Stimulation will be immediately increased from {0:0.0} mA to {1:0.0} mA. Do you want to continue?",
                        this.CurrentAmplitude,
                        this.TargetAmplitude);
                }

                if (this.SelectedRampSpeed?.RampSpeedItemName == RampSpeedItem.Slow)
                {
                    return "Amplitude adjustment will occur in 0.1 mA increments each second.";
                }

                if (this.SelectedRampSpeed?.RampSpeedItemName == RampSpeedItem.Medium)
                {
                    return "Amplitude adjustment will occur in 0.1 mA increments twice per second.";
                }

                return this.SelectedRampSpeed?.RampSpeedItemName == RampSpeedItem.Fast
                                            ? "Amplitude adjustment will occur in 0.5 mA increments each second."
                                            : string.Empty;
            }
            
        }

        public bool IsSelectedRampSpeedAsDefault => this.SelectedRampSpeed?.RampSpeedItemName != RampSpeedItem.Instant;

        public string Title => string.Empty;

        public ICommand CloseCommand { get; }

        public ICommand OkCommand { get; }

        public double tempAmp;
        public IEventAggregator _eventAggregator;

        public ViewAViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            this.amplitudeList = new ObservableAmplitudeList(_adpDC);
            this.CloseCommand = new DelegateCommand(this.CloseDialog);
            this.OkCommand = new DelegateCommand(this.RampUpAmplitude).ObservesCanExecute(() => this.CanExecuteOKCommand);

            this.RaisePropertyChanged(nameof(tempAmp));

            this.RampSpeedItemList = new ObservableCollection<RampSpeedModel>
                              {
                                new RampSpeedModel(RampSpeedItem.Slow,1),
                                new RampSpeedModel(RampSpeedItem.Medium, .5f),
                                new RampSpeedModel(RampSpeedItem.Fast, 1),
                                new RampSpeedModel(RampSpeedItem.Instant, 0)
                              };

            this.defaultRampSpeed = this.defaultRampSpeed != null
                                    ? this.RampSpeedItemList.FirstOrDefault(x => x.RampSpeedItemName == defaultRampSpeed.RampSpeedItemName)
                                    : this.RampSpeedItemList.FirstOrDefault(x => x.RampSpeedItemName == RampSpeedItem.Slow);

           // this.SelectedRampSpeed = this.defaultRampSpeed;

            _eventAggregator = eventAggregator;

            //_eventAggregator.GetEvent<ApplyTargetAmplitude>().Subscribe(Applytarget);
            _eventAggregator.GetEvent<ApplyRampSpeed>().Subscribe(ApplyRampspeed);
        }

        //private void Applytarget(double obj)
        //{
        //    TargetAmplitude = obj;
        //    var ind = amplitudeList.Where(x => x.AmplitudeValue == obj).FirstOrDefault();
        //    if(ind != null)
        //    {
        //        SelectedAmplitudeItemIndex = amplitudeList.IndexOf(ind);
        //    }
        //    else
        //    {
        //        SelectedAmplitudeItemIndex = 0;
        //    }
        //}

        private void ApplyRampspeed(string obj)
        {
            if(obj == "Slow (0.1 mA/sec)") 
            {
                SelectedRampSpeed = RampSpeedItemList.FirstOrDefault(x => x.RampSpeedItemName == RampSpeedItem.Slow);
            }
            else if (obj == "Medium (0.2 mA/sec)")
            {
                SelectedRampSpeed = RampSpeedItemList.FirstOrDefault(x => x.RampSpeedItemName == RampSpeedItem.Medium);
            }
            else if (obj == "Fast (0.5 mA/sec)")
            {
                SelectedRampSpeed = RampSpeedItemList.FirstOrDefault(x => x.RampSpeedItemName == RampSpeedItem.Fast);
            }
            else
            {
                SelectedRampSpeed = RampSpeedItemList.FirstOrDefault(x => x.RampSpeedItemName == RampSpeedItem.Instant);
            }
        }

        private void RampUpAmplitude()
        {
            bool settotarget = false;
            var parameters = new DialogParameters();

            TargetAmplitude = SetTargetAmplitude(SelectedAmplitudeItemIndex);

            if(TargetAmplitude !=0)
            {
                settotarget = true;
            }

            if (this.TargetAmplitude != null && this.TargetAmplitude != this.CurrentAmplitude)
            {
               
                if (this.SelectedRampSpeed.RampSpeedItemName == RampSpeedItem.Instant)
                {
                    
                    if (this.CurrentAmplitude < this.TargetAmplitude)
                    {
                        
                        this.stepSize = (int)((this.TargetAmplitude ) - (this.CurrentAmplitude )*10);
                    }
                    else
                    {
                        this.stepSize = (int)((this.CurrentAmplitude) - (this.TargetAmplitude )*10);
                    }
                    
                    //displayRampUI = false;
                }
                //else
                //{
                //    this.defaultRampSpeed = this.SelectedRampSpeed;
                //}

                parameters.Add("RampSpeed", this.SelectedRampSpeed.Name);
                parameters.Add("DisplayRampUI", settotarget);
                parameters.Add("StepSize", this.stepSize);
                parameters.Add("TargetAmplitude", (this.TargetAmplitude));
                parameters.Add(DialogNames.CurrentAmplitudeParameter, (this.CurrentAmplitude));
                parameters.Add("StepInterval", this.stepInterval);
            }

            if (this.IsSetRampSpeedAsDefault)
            {
                this.defaultRampSpeed = this.SelectedRampSpeed;
                //this.defaultRampSpeed = this.SelectedRampSpeed;
            }

            // Close the pop up
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameters));
        }

        public double SetTargetAmplitude(int index)
        {
            TargetAmplitude = amplitudeList[index].AmplitudeValue.Value;
            return TargetAmplitude;
        }

        private void UpdateRampSpeed()
        {
            if (this.SelectedRampSpeed.RampSpeedItemName != RampSpeedItem.Instant)
            {
                this.stepInterval = this.SelectedRampSpeed.Value;
            }
        }

        //private void UpdateRampSelection()
        //{
        //   // if 
        //}

        private void CloseDialog()
        {
            this.RequestClose?.Invoke(null);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            SelectedAmplitudeItemIndex = parameters.GetValue<int>("targerAmpIndex");
            double currentAmplitude = parameters.GetValue<double>(DialogNames.CurrentAmplitudeParameter);
           // this.SelectedRampSpeed = this.defaultRampSpeed != null
           //                 ? this.RampSpeedItemList.FirstOrDefault(x => x.RampSpeedItemName == this.defaultRampSpeed.RampSpeedItemName)
           //                 : this.RampSpeedItemList.FirstOrDefault(x => x.RampSpeedItemName == RampSpeedItem.Slow);

            // Current area amplitude
            this.CurrentAmplitude = currentAmplitude;

        }
    }
}
