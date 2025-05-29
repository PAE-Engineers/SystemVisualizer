using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace SystemVisualizer.Converters
{
    public class SizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ValueTuple<double, double> tuple)
            {
                return new Size(tuple.Item1, tuple.Item2);
            }

            //return DependencyProperty.UnsetValue; // Return UnsetValue instead of null to avoid binding issues
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Size size)
            {
                return (size.Width, size.Height);
            }
            //return DependencyProperty.UnsetValue; // Return UnsetValue instead of null to avoid binding issues
            return value;
        }
    }
}

