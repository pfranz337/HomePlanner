using System.Globalization;
using System.Windows;

namespace HPAdmin.UI.Converters;


public class BoolToVisibilityConverter : MarkupExtensionConverter
{
    public bool Invert { get; set; } = false;
    public bool Collapse { get; set; } = true;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var flag = false;

        if (value is bool b)
            flag = b;

        if (Invert)
            flag = !flag;

        return flag
            ? Visibility.Visible
            : (Collapse ? Visibility.Collapsed : Visibility.Hidden);
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Visibility v) 
            return DependencyProperty.UnsetValue;

        var result = v == Visibility.Visible;
        return Invert ? !result : result;
    }
}