using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Nodify
{
    public class ResizeDirectionToVisiblityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ResizeDirections resizeDirections && parameter is string param && Enum.TryParse(param, out ResizeDirections direction))
            {
                return resizeDirections.HasFlag(direction);
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 