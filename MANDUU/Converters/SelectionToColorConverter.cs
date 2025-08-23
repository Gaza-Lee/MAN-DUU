using System.Globalization;

namespace MANDUU.Converters
{
    public class SelectionToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int stationId && parameter is int selectedStationId)
            {
                return stationId == selectedStationId ? Color.FromArgb("#2196F3") : Colors.Transparent;
            }
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectionToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int stationId && parameter is int selectedStationId)
            {
                return stationId == selectedStationId ? Color.FromArgb("#E3F2FD") : Colors.Transparent;
            }
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}