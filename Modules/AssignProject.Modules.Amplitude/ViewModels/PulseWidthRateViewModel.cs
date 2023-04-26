using Prism.Mvvm;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using System.Windows.Input;
using System.Collections.ObjectModel;
using SharedResource;
using Prism.Events;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class PulseWidthRateViewModel : BindableBase 
    {
        protected IDialogService dialogService;

        public event Action<IDialogResult> RequestClose;

        private bool _btnREnabled;
        public bool btnREnabled
        {
            get { return _btnREnabled; }
            set
            {
                if (_btnREnabled == value)
                {
                    return;
                }
                _btnREnabled = value;
                RaisePropertyChanged(nameof(btnREnabled));
            }
        }

            private int? rateselect;
        public int? RateSelected
        {
            get
            {
                return rateselect;
            }
            set 
            {
                rateselect = value;
                RaisePropertyChanged("RateSelected");
            }
        }

        private int? pulsewidthselected;
        public int? PulseWidthSelected
        {
            get 
            {
                return pulsewidthselected;
            }
            set 
            {
                pulsewidthselected = value;
                RaisePropertyChanged("PulseWidthSelected");
            }
        }

        private static AssignDatabaseDataContext _adpDC = new AssignDatabaseDataContext();
        private  ObservableRateList rateList;
        private readonly ObservablePulseWidthList pulsewidthList;

        private readonly ObservableCollection<RateTable> rateTables;

        private readonly ObservableCollection<PulseWidthTable> pulseWidthTables;

        IEventAggregator _eventAggregator;

        public ICommand PulseWidthDialogCommand { get; set; }

        public ICommand RateDialogCommand { get; set; }

        public PulseWidthRateViewModel(IDialogService dialogservice, IEventAggregator eventAggregator)
        {
            rateList = new ObservableRateList(_adpDC);
            pulsewidthList = new ObservablePulseWidthList(_adpDC);
            
            this.dialogService = dialogservice;
            RateSelected = rateList[30].RateValue;
            PulseWidthSelected = pulsewidthList[4].PulseWidthValues;
            this.PulseWidthDialogCommand = new DelegateCommand(this.ShowPulseWidthDialog);
            this.RateDialogCommand = new DelegateCommand(this.ShowRateDialog);
            this.pulseWidthTables = new ObservableCollection<PulseWidthTable>();
            this.rateTables = rateList;

            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<ResetPulseWidthRateValue>().Subscribe(ResetRate);
            _eventAggregator.GetEvent<ResetPulseWidthRateValue>().Subscribe(ResetPulseWidth);
            _eventAggregator.GetEvent<ApplyRate>().Subscribe(applyrate);
            _eventAggregator.GetEvent<ApplyPulse>().Subscribe(applypulse);
        }

        private void applypulse(int obj)
        {
            PulseWidthSelected = obj;
        }

        private void applyrate(int obj)
        {
            RateSelected = obj;
        }

        private void ResetPulseWidth(int pw)
        {
            PulseWidthSelected = pw;
        }

        private void ResetRate(int obj)
        {
            RateSelected = obj;
        }

        private void ShowPulseWidthDialog()
        {
            if (PulseWidthSelected == null && PulseWidthSelected == pulsewidthList[4].PulseWidthValues)
            {
                return;
            }

            var pulse = this.PulseWidthSelected ?? 0;

            this.RaisePropertyChanged(nameof(pulse));

            var parameters = new DialogParameters { { DialogNames.PulseSelected, pulse} };

            int pulseresult = 60;

            this.dialogService.ShowDialog(DialogNames.PulseWidthOptionDialog,parameters,p=> p.Parameters.TryGetValue<int>(DialogNames.PulseSelected,out pulseresult));

            PulseWidthSelected = pulseresult;
            this.RaisePropertyChanged(nameof(PulseWidthSelected));

            _eventAggregator.GetEvent<SavePulse>().Publish(pulseresult);
        }

        private void ShowRateDialog()
        {
            if (RateSelected == null && RateSelected == rateList[30].RateValue)
            {
                return;
            }

            var currentValue = this.RateSelected ?? 0;

            this.RaisePropertyChanged(nameof(currentValue));

            var parameters = new DialogParameters {
                {DialogNames.RateSelected, currentValue },
                {DialogNames.RateSaveButtonEnable, btnREnabled }
            };

            int rateresult=130;
            bool tempenable = false;

            dialogService.ShowDialog(DialogNames.RateSelectDialog, parameters,r => { r.Parameters.TryGetValue<int>(DialogNames.RateSelected, out rateresult); r.Parameters.TryGetValue<bool>(DialogNames.RateSaveButtonEnable, out tempenable); });

            RateSelected = rateresult;
            btnREnabled = tempenable;
            this.RaisePropertyChanged(nameof(RateSelected));
            this.RaisePropertyChanged(nameof(btnREnabled));

            _eventAggregator.GetEvent<SaveRate>().Publish(rateresult);

            if (btnREnabled == true)
            {
                _eventAggregator.GetEvent<EnableSaveButtonEvent>().Publish(btnREnabled);
            }
        }
    }
}
