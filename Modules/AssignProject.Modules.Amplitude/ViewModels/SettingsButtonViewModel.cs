using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using SharedResource;
using System.Windows.Input;
using Prism.Services.Dialogs;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class SettingsButtonViewModel : BindableBase
    {
        private IDialogService dialogService;
        IEventAggregator _eventAggregator;

        private bool _btnEnabled;
        private string hemisphere;
        private string port;
        private string lead;

        public bool btnEnabled
        {
            get { return _btnEnabled; }
            set
            {
                if (_btnEnabled == value)
                {
                    return;
                }
                _btnEnabled = value;
                RaisePropertyChanged(nameof(btnEnabled));
            }
        }

        public double TotalCurrent { get; set; }

        public int RateSelected { get; set; }

        public int PulseWidthSelected { get; set; }

        public PulseWidthRateViewModel PulseWidthRate { get; set; }
        public ICommand RevertCommand { get; private set; }

        
        public ICommand SaveCommand { get; private set; }

        private double currentAmp;
        public double CurrentAmp { 
            get=> currentAmp; private set {
                currentAmp = value;
                RaisePropertyChanged(nameof(CurrentAmp));
            } }
        public double TargetAmp { get; private set; }
        public string RampSpeed { get; private set; }
        public int PulseWidth { get; private set; }
        public int Rates { get; private set; }

        public SettingsButtonViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            btnEnabled = true;
            RevertCommand = new DelegateCommand(DisableControlHandler);
            SaveCommand = new DelegateCommand(SaveValueHandler);
            RaisePropertyChanged(nameof(btnEnabled));
            this.dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<EnableSaveButtonEvent>().Subscribe(EnableSaveButton);

            _eventAggregator.GetEvent<SaveCurrentAmplitude>().Subscribe(savecurrentamp);
            _eventAggregator.GetEvent<SaveTargetAmplitude>().Subscribe(saveTargetamp);
            _eventAggregator.GetEvent<SaveRampSpeed>().Subscribe(saverampspeed);
            _eventAggregator.GetEvent<SavePulse>().Subscribe(pulseresult);
            _eventAggregator.GetEvent<SaveRate>().Subscribe(rateresult);
            _eventAggregator.GetEvent<Savehemisphere>().Subscribe(hemispheresave);
            _eventAggregator.GetEvent<SaveLead>().Subscribe(leadsave);
            _eventAggregator.GetEvent<SavePort>().Subscribe(portsave);
        }

        private void EnableSaveButton(bool obj)
        {
            btnEnabled = obj;
        }

        private void portsave(string obj)
        {
            port = obj;
        }

        private void leadsave(string obj)
        {
            lead = obj;
        }

        private void hemispheresave(string obj)
        {
            hemisphere = obj;
        }

        private void rateresult(int obj)
        {
            Rates = obj;
        }

        private void pulseresult(int obj)
        {
            PulseWidth = obj;
        }

        private void saverampspeed(string obj)
        {
            RampSpeed = obj;
        }

        private void saveTargetamp(double obj)
        {
            TargetAmp = obj;
        }

        private void savecurrentamp(double obj)
        {
            CurrentAmp = obj;
        }

        private void SaveValueHandler()
        {
            

            var savepar = new DialogParameters();
            savepar.Add("SaveCurrentAmp", CurrentAmp);
            savepar.Add("saveTargetAmp", TargetAmp);
            savepar.Add("saverampspeed", RampSpeed);
            savepar.Add("savePulse", PulseWidth);
            savepar.Add("saverate", Rates);
            savepar.Add("savehemisphere", hemisphere);
            savepar.Add("savelead", lead);
            savepar.Add("saveport", port);

            this.dialogService.ShowDialog(DialogNames.RateSetting, savepar, null);
        }

        

        private void DisableControlHandler()
        {
            if(btnEnabled == true)
            {
                btnEnabled = false;
            }

            RateSelected = 0;
            PulseWidthSelected = 0;
            TotalCurrent = 0.0;

            _eventAggregator.GetEvent<ResetPulseWidthRateValue>().Publish(RateSelected);
            _eventAggregator.GetEvent<ResetPulseWidthRateValue>().Publish(PulseWidthSelected);
            _eventAggregator.GetEvent<ResetAmplitudeValue>().Publish(TotalCurrent);
        }
    }
}
