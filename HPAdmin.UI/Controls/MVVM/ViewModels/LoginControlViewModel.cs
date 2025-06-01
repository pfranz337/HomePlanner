using AutoMapper;
using HPAdmin.Data.DbContext;
using HPAdmin.UI.ViewModels.Base;

namespace HPAdmin.UI.Controls.MVVM.ViewModels;

public class LoginControlViewModel : ViewModelBase
{
    //action budou mozna odstreny
    public event Action<string>? LoginSucceeded;
    public event Action? LogoutRequested;



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
        //todo over uzivatele proti db a nastavit jako aktivniho uzivatele do session nebo podobne
        LoginSucceeded?.Invoke(UserName!);//misto tohle by melo byt nastaveni uzivatele do session nebo podobneho uloziste, aby byl dostupny v cele aplikaci
        IsLoggedIn = true;//pak tenhle priznak by nemel byt potreba protze se muzu podivat do sesion
    }

    private void onLogout()
    {
        //todo odstranit uzivatele z session nebo podobne
        IsLoggedIn = false;
        LogoutRequested?.Invoke(); //misto tohodle by se mel zavolat logout na session nebo podobnem ulozisti, prepnuti na login control resit jinak (EventAgregator???)
    }
}