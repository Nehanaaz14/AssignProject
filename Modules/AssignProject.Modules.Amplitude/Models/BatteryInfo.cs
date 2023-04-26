using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace AssignProject.Modules.Amplitude.Models
{
    public sealed class BatteryInfo : IDisposable
    {
        private const int CriticalBatteryThreshold = 12;

        private const int LowBatteryThreshold = 20;

        private const int MonitoringInterval = 1000;

        private static readonly object SyncRoot = new object();

        private readonly int batteryCritical;

        private readonly int LowThreshold;

        private readonly System.Timers.Timer batteryMonitorTimer;

        private bool LastMonitoredAtCritical;

        private bool LastMonitoredAtLow;

        public event EventHandler BatteryInfoUpdated;
        public bool BatteryToLow { get; private set; }

        public bool BatteryToCritical { get; private set; }

        public bool IsCharging { get; private set; }

        public int BatteryPercent { get; private set; }

        private bool isPercentGreaterThanEighty;

        private bool isPercentGreaterThanSixty;

        private bool isPercentGreaterThanForty;

        private bool isPercentGreaterThanTwenty;

        private bool isPercentGreaterThanThreshold;

        private bool isPercentGreaterThanCritical;

        private bool isPercentLessThanCritical;
       
        private static volatile BatteryInfo instance;

        public static BatteryInfo Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                lock (SyncRoot)
                {
                    if (instance == null)
                    {
                        instance = new BatteryInfo();
                    }
                }

                return instance;
            }
        }

        private BatteryInfo()
        {
            this.LowThreshold = BoundPercentage(LowBatteryThreshold);
            this.batteryCritical = BoundPercentage(CriticalBatteryThreshold);
            var interval = MonitoringInterval;

            SystemEvents.PowerModeChanged += this.SystemEventsPowerModeChanged;
            this.batteryMonitorTimer = new Timer(interval);
            this.batteryMonitorTimer.Elapsed += this.BatteryMonitorTimerOnElapsed;

            this.BatteryInfoUpdated?.Invoke(this, null);

            this.batteryMonitorTimer.Start();
        }

        ~BatteryInfo()
        {
            this.Dispose(false);
        }

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static int BoundPercentage(int percentage)
        {
            return Math.Min(100, Math.Max(0, percentage));
        }

        private void BatteryMonitorTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            this.UpdateBatteryInformation();
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || this.IsDisposed)
            {
                return;
            }

            SystemEvents.PowerModeChanged -= this.SystemEventsPowerModeChanged;
            this.batteryMonitorTimer.Elapsed -= this.BatteryMonitorTimerOnElapsed;
            this.batteryMonitorTimer.Stop();
            this.batteryMonitorTimer.Dispose();
            this.IsDisposed = true;
        }


        /// <summary>Executed when the power mode changes.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The argument.</param>
        private void SystemEventsPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            this.UpdateBatteryInformation();
        }

        private void UpdateBatteryInformation()
        {
            if (this.IsDisposed)
            {
                return;
            }

            var batteryPercentage = BoundPercentage(
                (int)Math.Round(
                    SystemInformation.PowerStatus.BatteryLifePercent * 100f,
                    MidpointRounding.AwayFromZero));
            var isPluggedIn = SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online;

            this.UpdateBatteryInformation(batteryPercentage, isPluggedIn);
        }

        internal void UpdateBatteryInformation(int batteryPercentage, bool isPluggedIn)
        {
            var isPluggedInOld = this.IsCharging;
            this.IsCharging = isPluggedIn;
            this.BatteryPercent = batteryPercentage;

            var batteryCurrentlyAtLow = !this.IsCharging && this.BatteryPercent <= this.LowThreshold
                                                          && this.BatteryPercent > this.batteryCritical;
            var batteryCurrentlyAtCritical =
                !this.IsCharging && this.BatteryPercent <= this.batteryCritical;

            this.BatteryToLow = !this.LastMonitoredAtLow && batteryCurrentlyAtLow;
            this.BatteryToCritical = !this.LastMonitoredAtCritical && batteryCurrentlyAtCritical;
            this.LastMonitoredAtLow = batteryCurrentlyAtLow;
            this.LastMonitoredAtCritical = batteryCurrentlyAtCritical;

            var isBatteryStateChanged = this.isPercentGreaterThanEighty != this.BatteryPercent >= 80;

            this.isPercentGreaterThanEighty = this.BatteryPercent >= 80;
            if (this.isPercentGreaterThanSixty != this.BatteryPercent >= 60)
            {
                isBatteryStateChanged = true;
            }

            this.isPercentGreaterThanSixty = this.BatteryPercent >= 60;
            if (this.isPercentGreaterThanForty != this.BatteryPercent >= 40)
            {
                isBatteryStateChanged = true;
            }

            this.isPercentGreaterThanForty = this.BatteryPercent >= 40;

            if (this.isPercentGreaterThanTwenty != this.BatteryPercent > 20)
            {
                isBatteryStateChanged = true;
            }

            this.isPercentGreaterThanTwenty = this.BatteryPercent > 20;
            if (this.isPercentGreaterThanThreshold != this.BatteryPercent >= this.LowThreshold)
            {
                isBatteryStateChanged = true;
            }

            this.isPercentGreaterThanThreshold = this.BatteryPercent >= this.LowThreshold;
            if (this.isPercentGreaterThanCritical != this.BatteryPercent >= this.batteryCritical)
            {
                isBatteryStateChanged = true;
            }

            this.isPercentGreaterThanCritical = this.BatteryPercent >= this.batteryCritical;
            if (this.isPercentLessThanCritical != this.BatteryPercent < this.batteryCritical)
            {
                isBatteryStateChanged = true;
            }

            this.isPercentLessThanCritical = this.BatteryPercent < this.batteryCritical;

            isBatteryStateChanged = isBatteryStateChanged || isPluggedInOld != this.IsCharging;

            if (isBatteryStateChanged || this.BatteryToLow || this.BatteryToCritical)
            {
                this.BatteryInfoUpdated?.Invoke(this, null);
            }
        }
    }
}
