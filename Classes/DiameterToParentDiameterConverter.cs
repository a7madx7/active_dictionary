using System;
using System.Globalization;
using System.Windows.Data;

namespace Active_Dictionary.Classes
{
    internal class DiameterToParentDiameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (string) value;
            return double.Parse(temp) - 10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}