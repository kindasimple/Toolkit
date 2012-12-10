using System;
using System.Windows;
#if WINDOWS_PHONE
using System.Windows.Data;
#elif NETFX_CORE
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
#endif
namespace Kindasimple.Toolkit
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
#if WINDOWS_PHONE
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#elif NETFX_CORE
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            if (value == null || ((bool)value) == false)
            {
                return (parameter == null)
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
            else
            {
                return (parameter == null)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

#if WINDOWS_PHONE
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#elif NETFX_CORE
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#endif
        {
            throw new NotImplementedException();
        }
    }
}
