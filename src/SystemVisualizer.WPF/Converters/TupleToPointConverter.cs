using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SystemVisualizer.WPF.Converters
{
    class TupleToPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ValueTuple<double, double> tuple)
            {
                return new Point(tuple.Item1, tuple.Item2);
            }
            return DependencyProperty.UnsetValue; // Return UnsetValue instead of null to avoid binding issues
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Point point)
            {
                return (point.X, point.Y);
            }
            return DependencyProperty.UnsetValue; // Return UnsetValue instead of null to avoid binding issues
        }
    }
}
