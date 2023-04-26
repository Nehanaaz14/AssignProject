using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class RateSettingViewModel : BindableBase, IDialogAware
    {
        AssignDatabaseDataContext assign = new AssignDatabaseDataContext();
        private ObservableSavedSettingsList savedSettings;
        public int RateSelected { get; set; }
         public int PulseWidthSelected { get; set; }

        public double CurrentAmplitude { get; set; }

        public double TargetAmplitude { get; set; }

        public string ID { get; set; }

        public string RampSpeed { get; set; }

        public string hemisphere { get; set; }

        public string port { get; set; }

        public string lead { get; set; }

        private int _rating;

        public DelegateCommand ConfirmButtonCommand { get; set; }

        public DelegateCommand setFour { get; set; }
        public DelegateCommand setThree { get; set; }
        public DelegateCommand setTwo { get; set; }
        public DelegateCommand setOne { get; set; }
        public DelegateCommand setZero { get; set; }


        public int rating
        {
            get => _rating;
            set
            {
                _rating = value;
                RaisePropertyChanged(nameof(rating));
            }
        }

        public RateSettingViewModel()
        {
            this.ConfirmButtonCommand = new DelegateCommand(ConfirmSettingsHandler);
            savedSettings = new ObservableSavedSettingsList(assign);
            this.setFour = new DelegateCommand(SetFourHandler);
            this.setThree = new DelegateCommand(SetThreeHandler);
            this.setTwo = new DelegateCommand(SetTwoHandler);
            this.setOne = new DelegateCommand(SetOneHandler);
            this.setZero = new DelegateCommand(SetZeroHandler);
        }

        private void SetZeroHandler()
        {
            this.ratingselected(0);
        }

        private void SetOneHandler()
        {
            this.ratingselected(1);
        }

        private void SetTwoHandler()
        {
            this.ratingselected(2);
        }

        private void SetThreeHandler()
        {
            this.ratingselected(3);
        }

        private void SetFourHandler()
        {
            this.ratingselected(4);
        }

        private void createID()
        {
            string idname = "Settings";
            ID = idname + DateTime.Now.ToString();
        }

        private void ConfirmSettingsHandler()
        {
            createID();
            using(assign)
            {
                SavedSetting savedSetting = new SavedSetting();
                savedSetting.ID = ID;
                savedSetting.MinAmplitude = CurrentAmplitude;
                savedSetting.MaxAmplitude = TargetAmplitude;
                savedSetting.RampSpeed = RampSpeed;
                savedSetting.Pulse_Width = PulseWidthSelected;
                savedSetting.Rate = RateSelected;
                savedSetting.SHemisphere = hemisphere;
                savedSetting.SLeadType = lead;
                savedSetting.SPortType = port;
                savedSetting.Rating = rating;
                assign.SavedSettings.InsertOnSubmit(savedSetting);
                assign.SubmitChanges();
            }
            MessageBox.Show("Settings Saved Successfully", "Saved Settings", MessageBoxButton.OK);
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        private int ratingselected(int sel)
        {
            rating = sel;
            RaisePropertyChanged(nameof(rating));
            return rating;
        }

        public string Title => "";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel)); // throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            CurrentAmplitude = parameters.GetValue<double>("SaveCurrentAmp");
            TargetAmplitude = parameters.GetValue<double>("saveTargetAmp");
            RampSpeed = parameters.GetValue<string>("saverampspeed");
            RateSelected = parameters.GetValue<int>("saverate");
            PulseWidthSelected = parameters.GetValue<int>("savePulse");
            hemisphere = parameters.GetValue<string>("savehemisphere");
            port = parameters.GetValue<string>("saveport");
            lead = parameters.GetValue<string>("savelead");
        }
    }
}
