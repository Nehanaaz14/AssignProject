using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AssignProject.Modules.Amplitude.Converter
{
    public class BatteryChargeColor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is int charge) || !(values[1] is bool isCharging))
            {
                return Brushes.Black;
            }

            if (charge <= 20 && !isCharging)
            {
                return Brushes.Red;
            }

            return Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
