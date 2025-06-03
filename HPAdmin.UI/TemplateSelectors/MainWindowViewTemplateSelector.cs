using HPAdmin.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace HPAdmin.UI.TemplateSelectors;

public class MainWindowViewTemplateSelector : DataTemplateSelector
{
    public DataTemplate? LoginTemplate { get; set; }
    public DataTemplate? TasksTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        if (item is MainWindowViewType viewType)
        {
            return viewType switch
            {
                MainWindowViewType.Login => LoginTemplate,
                MainWindowViewType.Tasks => TasksTemplate,
                _ => base.SelectTemplate(item, container),
            };
        }

        return base.SelectTemplate(item, container);
    }
}