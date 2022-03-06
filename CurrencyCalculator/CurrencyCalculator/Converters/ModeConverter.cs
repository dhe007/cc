using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CurrencyRateCalculator.Converters
{
    /// <summary>
    /// Converters, to deal with the data binding Converting jobs
    /// </summary>
    public class ModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool status = (bool)value;
            if (status)
                return "Offline Mode";
            else
                return "Online Mode";


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
