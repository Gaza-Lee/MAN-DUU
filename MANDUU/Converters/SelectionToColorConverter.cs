using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;

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
                if (stationId == selectedStationId)
                {
                    if (Application.Current.RequestedTheme == AppTheme.Light)
                    {
                        return Color.FromArgb("#2196F3");
                    }
                    else
                    {
                        return Color.FromArgb("#1E88E5");
                    }
                }
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
                if (stationId == selectedStationId)
                {
                    if (Application.Current.RequestedTheme == AppTheme.Light)
                    {
                        return Color.FromArgb("#E3F2FD");
                    }
                    else
                    {
                        return Color.FromArgb("#B3E5FC");
                    }
                }
            }

            return Colors.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}