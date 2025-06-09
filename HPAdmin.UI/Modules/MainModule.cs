using System.Windows;
using HomePlanner.UI.Controls.MVVM.Views;
using HomePlanner.UI.Helpers;
using HomePlanner.UI.Views;

namespace HomePlanner.UI.Modules;

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