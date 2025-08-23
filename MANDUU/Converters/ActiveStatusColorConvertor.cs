using MANDUU.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Converters
{
    public class ActiveStatusColorConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is PrintingStationStatus status)
            {
                return status == PrintingStationStatus.Active ?
                    Color.FromArgb("#4CAF50") : // Green for Active
                    Color.FromArgb("#9E9E9E");  // Gray for Inactive
            }
            return Colors.Gray;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
