using System.Windows;
using System.Windows.Controls;

namespace HomePlanner.UI.Views.Base
{
    public interface IView
    {
    }

    public abstract class BaseControlView : UserControl, IView
    {
    }

    public abstract class BaseWindowView : Window, IView
    {
    }
}
