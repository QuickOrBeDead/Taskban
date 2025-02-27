﻿namespace Taskban.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class StringToVisibleIfNotNullOrWhiteSpaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}