using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Converters
{
    public class SelectedPaymentMethodBackgroundConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return Colors.Transparent;

            var actualValue = value.ToString();
            var expectedValue = parameter.ToString();

            return actualValue == expectedValue
                ? Application.Current?.Resources["Primary"] ?? Colors.Blue // Selected
                : Colors.Transparent; // Unselected
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
