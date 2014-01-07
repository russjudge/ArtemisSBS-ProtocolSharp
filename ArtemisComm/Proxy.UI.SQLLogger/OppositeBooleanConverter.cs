using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ArtemisComm.Proxy.UI.SQLLogger
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class OppositeBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? val = value as bool?;
            return !(val == true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? val = value as bool?;
            return !(val == true);
        }
    }
}
