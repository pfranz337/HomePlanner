using System.Windows.Markup;
using HomePlanner.Localizations;

namespace HomePlanner.UI.MarkupExtensions;

public class LocalizationExtension : MarkupExtension
{
    public string Key { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return LocalizationManager.GetString(Key);
    }
}