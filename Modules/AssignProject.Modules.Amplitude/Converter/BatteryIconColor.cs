using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SharedResource;

namespace AssignProject.Modules.Amplitude.Converter
{
    public class BatteryIconColor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is int batteryPercent))
            {
                return null;
            }

            if (!(values[1] is bool isCharging))
            {
                return null;
            }

            if (isCharging)
            {
                if (batteryPercent > 80)
                {
                    return StaticResource.ChargingBattery100;
                }

                if (batteryPercent > 60)
                {
                    return StaticResource.ChargingBattery80;
                }

                if (batteryPercent > 40)
                {
                    return StaticResource.ChargingBattery60;
                }

                if (batteryPercent > 17)
                {
                    return StaticResource.ChargingBattery40;
                }

                if (batteryPercent > 12)
                {
                    return StaticResource.ChargingBattery20;
                }

                return StaticResource.ChargingBattery10;
            }

            if (batteryPercent > 80)
            {
                return StaticResource.Battery100;
            }

            if (batteryPercent > 60)
            {
                return StaticResource.Battery80;
            }

            if (batteryPercent > 40)
            {
                return StaticResource.Battery60;
            }

            if (batteryPercent > 17)
            {
                return StaticResource.Battery40;
            }

            if (batteryPercent > 12)
            {
                return StaticResource.Battery20;
            }

            return StaticResource.Battery10;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
