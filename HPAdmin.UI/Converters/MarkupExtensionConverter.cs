using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace HomePlanner.UI.Converters;

[MarkupExtensionReturnType(typeof(IValueConverter))]
public abstract class MarkupExtensionConverter : MarkupExtension, IValueConverter
{
    public abstract object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture);
    public abstract object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture);
}