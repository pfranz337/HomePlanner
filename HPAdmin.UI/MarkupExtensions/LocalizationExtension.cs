using System.Windows.Markup;
using HPAdmin.Localizations;

namespace HPAdmin.UI.MarkupExtensions;

public class LocalizationExtension : MarkupExtension
{
    public string Key { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return LocalizationManager.GetString(Key) ?? string.Empty;
    }
}