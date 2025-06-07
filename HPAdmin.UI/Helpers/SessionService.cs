namespace HPAdmin.UI.Helpers;

public class SessionService
{
    public class LoginInfo(string userName) : EventArgs
    {
        public string UserName { get; } = userName;
    }

    public event EventHandler<LoginInfo> OnLogin;
    public event EventHandler OnLogout;

    public string UserName { get; private set; }

    public bool IsLoggedIn { get; private set; }

    public void Login(string userName)
    {
        IsLoggedIn = true;
        UserName = userName;
        OnLogin?.Invoke(this, new LoginInfo(userName));
    }

    public void Logout(string userName)
    {
        IsLoggedIn = false;
        UserName = null;
        OnLogout?.Invoke(this, EventArgs.Empty);
    }
}