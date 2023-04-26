using AssignProject.Modules.Amplitude.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Prism.Events;
using SharedResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class MainHeaderViewModel : BindableBase, IDisposable , INavigationAware
    {
        public IEventAggregator _aggregator;

        public bool enableViewCommamnd;

        public bool EnableViewCommamnd
        {
            get => enableViewCommamnd;
            set
            {
                enableViewCommamnd = value;
                RaisePropertyChanged(nameof(enableViewCommamnd));
            }
        }

        public bool enableSettingsCommamnd;

        public bool EnableSettingsCommamnd
        {
            get => enableSettingsCommamnd;
            set
            {
                enableSettingsCommamnd = value;
                RaisePropertyChanged(nameof(enableViewCommamnd));
            }
        }

        private bool isCharging;
        public bool IsCharging
        {
            get => this.isCharging;
            set => this.SetProperty(ref this.isCharging, value);
        }

        private int batteryPercent;
        public int BatteryPercent
        {
            get => this.batteryPercent;
            set => this.SetProperty(ref this.batteryPercent, value);
        }

        private readonly BatteryInfo batteryInfo;

        private bool IsDisposed { get; set; }

        private readonly IRegionManager _regionManager;

        private readonly IDialogService dialogService;
        public DelegateCommand appSettingCommand { get; set; }

        public DelegateCommand viewSettingCommand { get; set; }
        public MainHeaderViewModel(IRegionManager regionManager, IDialogService dialogservice,IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            this.dialogService = dialogservice;
            _aggregator = eventAggregator;
            this.EnableViewCommamnd = true;
            this.EnableSettingsCommamnd = true;
            appSettingCommand = new DelegateCommand(ApplicationSettingHandler);
            viewSettingCommand = new DelegateCommand(ViewSettingHandler);
            this.batteryInfo = BatteryInfo.Instance;
            this.batteryInfo.BatteryInfoUpdated += BatteryInfo_BatteryInfoUpdated;

            _aggregator.GetEvent<EnableViewButtonEvent>().Subscribe(Viewenable);
            _aggregator.GetEvent<EnableViewButton>().Subscribe(Viewenable);
            _aggregator.GetEvent<EnableSettingButtonEvent>().Subscribe(Settingenable);
        }

        private void Settingenable(bool obj)
        {
            EnableSettingsCommamnd = obj;
        }

        private void Viewenable(bool obj)
        {
            EnableViewCommamnd = obj;
        }

        private void BatteryInfo_BatteryInfoUpdated(object sender, EventArgs e)
        {
            this.BatteryPercent = this.batteryInfo.BatteryPercent;
            this.IsCharging = this.batteryInfo.IsCharging;

            if(this.batteryInfo.BatteryToLow)
            {
                // this.dialogService.ShowDialog();
                MessageBox.Show("Please charge battery","Battery Level Low",MessageBoxButton.OK);
            }
            else if(this.batteryInfo.BatteryToCritical)
            {
                MessageBox.Show("Please recharge battery immediately", "Battery Level Critical",MessageBoxButton.OK);
            }
        }

        private void ApplicationSettingHandler()
        {
            if(EnableSettingsCommamnd)
            {
                EnableSettingsCommamnd = false;
            }
            if (EnableViewCommamnd)
            {
                EnableViewCommamnd = false;
            }

            _regionManager.RequestNavigate(RegionNames.SettingsDisplayRegion,"ApplicationSettings");
        }

        private void ViewSettingHandler()
        {
            if(EnableViewCommamnd)
            {
                EnableViewCommamnd = false;
            }
            if (EnableSettingsCommamnd)
            {
                EnableSettingsCommamnd = false;
            }
            _regionManager.RequestNavigate(RegionNames.SettingsDisplayRegion, "SavedSettings");
           
           
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || this.IsDisposed)
            {
                return;
            }

            this.batteryInfo.BatteryInfoUpdated -= this.BatteryInfo_BatteryInfoUpdated;
            this.batteryInfo.Dispose();
            this.IsDisposed = true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _regionManager.RequestNavigate(RegionNames.SettingsDisplayRegion, "ApplicationSettings");
            _regionManager.RequestNavigate(RegionNames.SettingsDisplayRegion, "SavedSettings");
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
