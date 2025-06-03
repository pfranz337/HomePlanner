using AutoMapper;
using HPAdmin.Data.DbContext;
using HPAdmin.UI.EventAgregators;
using HPAdmin.UI.Heleprs;
using HPAdmin.UI.ViewModels.Base;

namespace HPAdmin.UI.Controls.MVVM.ViewModels;

public class LoginControlViewModel : ViewModelBase
{
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



    private void onLogin()
    {
        DIHelper.Instance.EventAggregator.GetEvent<OnLoginEvent>().Publish(LoggedUserName);
        IsLoggedIn = true;
    }

    private void onLogout()
    {
        IsLoggedIn = false;
        DIHelper.Instance.EventAggregator.GetEvent<OnLogoutEvent>().Publish();
    }
}