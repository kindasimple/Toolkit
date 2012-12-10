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
    public class IsNonEmptyConverter : IValueConverter
    {
#if WINDOWS_PHONE
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#elif NETFX_CORE
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            return !string.IsNullOrWhiteSpace(value.ToString());
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
