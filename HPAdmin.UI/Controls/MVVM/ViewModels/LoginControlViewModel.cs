using AutoMapper;
using HPAdmin.Data.DbContext;
using HPAdmin.UI.Controls.MVVM.Views;
using HPAdmin.UI.Helpers;
using HPAdmin.UI.ViewModels.Base;

namespace HPAdmin.UI.Controls.MVVM.ViewModels;

public class LoginControlViewModel : ViewModelBase, IRegionMemberLifetime, INavigationAware
{ 
    public bool KeepAlive => true;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    private bool isLoggedIn;
    public bool IsLoggedIn
    {
        get => isLoggedIn;
        set => SetProperty(ref isLoggedIn, value);
    }

    public string? LoggedUserName { get; set; }

    public DelegateCommand LoginCommand { get; }

    public DelegateCommand LogoutCommand { get; }



    public LoginControlViewModel(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
        LoginCommand = new DelegateCommand(onLogin);
        LogoutCommand = new DelegateCommand(onLogout);
    }



    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (navigationContext.Parameters.TryGetValue("IsLogout", out bool isLogout))
        {
            IsLoggedIn = !isLogout;
        }
    }

    public bool IsNavigationTarget(NavigationContext navigationContext) => true;

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }



    private void onLogin()
    {
        //DIHelper.Instance.EventAggregator.GetEvent<OnLoginEvent>().Publish(LoggedUserName);
        DIHelper.Instance.SessionService.Login(UserName ?? string.Empty);
        DIHelper.Instance.RegionManager.RequestNavigate(RegionNames.MainRegion, nameof(TasksControlView));
        IsLoggedIn = true;
    }

    private void onLogout()
    {
        //DIHelper.Instance.EventAggregator.GetEvent<OnLogoutEvent>().Publish();
        DIHelper.Instance.SessionService.Logout(UserName ?? string.Empty);
        IsLoggedIn = false;
    }
}