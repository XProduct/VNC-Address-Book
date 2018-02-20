using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VncAddressBook.Converters
{
    public class BoolToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            Visibility result;
            if ((bool)value)
            {
                result = Visibility.Visible;
                if (System.Convert.ToInt16(parameter) == 1)
                {
                    result = Visibility.Collapsed;
                }
            }
            else
            {
                result = Visibility.Collapsed;
                if (System.Convert.ToInt16(parameter) == 1)
                {
                    result = Visibility.Visible;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }
}
