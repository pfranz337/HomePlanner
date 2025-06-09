using AutoMapper;
using HomePlanner.Data.DbContext;
using HomePlanner.UI.Controls.MVVM.Views;
using HomePlanner.UI.Helpers;
using HomePlanner.UI.ViewModels.Base;

namespace HomePlanner.UI.Controls.MVVM.ViewModels;

public class LoginControlViewModel : ViewModelBase, IRegionMemberLifetime, INavigationAware
{ 
    public bool KeepAlive => true;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public bool IsLoggedIn => DIHelper.Instance.SessionService.IsLoggedIn;

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
        RaisePropertyChanged(nameof(IsLoggedIn));
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
    }

    private void onLogout()
    {
        //DIHelper.Instance.EventAggregator.GetEvent<OnLogoutEvent>().Publish();
        DIHelper.Instance.SessionService.Logout();
        RaisePropertyChanged(nameof(IsLoggedIn));
    }
}