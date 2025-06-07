using HPAdmin.UI.Controls.MVVM.Views;

namespace HPAdmin.UI.Modules;

public class TasksModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<TasksControlView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}