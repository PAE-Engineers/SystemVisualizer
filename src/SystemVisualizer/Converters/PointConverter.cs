using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace SystemVisualizer.Converters
{
    class PointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ValueTuple<double, double> tuple)
            {
                return new Point(tuple.Item1, tuple.Item2);
            }
            //return DependencyProperty.UnsetValue; // Return UnsetValue instead of null to avoid binding issues
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Point point)
            {
                return (point.X, point.Y);
            }
            //return DependencyProperty.UnsetValue; // Return UnsetValue instead of null to avoid binding issues
            return value;
        }
    }
}
