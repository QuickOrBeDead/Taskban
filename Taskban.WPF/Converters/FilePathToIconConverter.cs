namespace Taskban.WPF.Converters
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    public class FilePathToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var filePath = (string)value;
            using var iconForFile = GetIconForFile(filePath);

            return Imaging.CreateBitmapSourceFromHIcon(iconForFile.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        private static Icon GetIconForFile(string filePath)
        {
            var defaultIcon = SystemIcons.WinLogo;
            var iconForFile = defaultIcon;
            if (filePath != null && File.Exists(filePath))
            {
                try
                {
                    iconForFile = Icon.ExtractAssociatedIcon(filePath) ?? defaultIcon;
                }
                catch
                {
                    iconForFile = defaultIcon;
                }
            }

            return iconForFile;
        }
    }
}