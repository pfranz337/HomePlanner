using System.Windows;
using HPAdmin.UI.Controls.MVVM.Views;
using HPAdmin.UI.Views;
using HPAdmin.UI.Helpers;

namespace HPAdmin.UI.Modules;

public class MainModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainWindow>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        Application.Current.Dispatcher.BeginInvoke(() =>
        {
            DIHelper.Instance.RegionManager.RequestNavigate(RegionNames.MainRegion, nameof(LoginControlView));
        });
    }
}