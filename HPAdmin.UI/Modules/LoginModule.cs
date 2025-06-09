using HomePlanner.UI.Controls.MVVM.Views;

namespace HomePlanner.UI.Modules;

public class LoginModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<LoginControlView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}