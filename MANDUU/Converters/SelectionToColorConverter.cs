using System.Globalization;
using Microsoft.Maui.Controls;

namespace MANDUU.Converters
{
    public class StationSelectionToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return Colors.Transparent;

            if (values[0] is int stationId && values[1] is int selectedStationId)
            {
                return stationId == selectedStationId
                    ? Color.FromArgb("#2196F3")
                    : Colors.Transparent;
            }

            return Colors.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class StationSelectionToBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return Colors.Transparent;

            if (values[0] is int stationId && values[1] is int selectedStationId)
            {
                return stationId == selectedStationId
                    ? Color.FromArgb("#E3F2FD")
                    : Colors.Transparent;
            }

            return Colors.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
