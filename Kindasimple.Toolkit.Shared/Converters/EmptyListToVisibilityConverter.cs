using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
#if WINDOWS_PHONE
using System.Windows.Data;
#elif NETFX_CORE
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
#endif
namespace Kindasimple.Toolkit
{
    public class EmptyListToVisibilityConverter : IValueConverter
    {
#if WINDOWS_PHONE
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#elif NETFX_CORE
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            if (value == null || ((IList)value).Count == 0)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
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
