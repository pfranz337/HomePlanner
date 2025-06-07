using AutoMapper;
using HPAdmin.Data.DbContext;
using HPAdmin.UI.Controls.MVVM.Views;
using HPAdmin.UI.Helpers;
using HPAdmin.UI.ViewModels.Base;

namespace HPAdmin.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string? _loggedUserName;
        private bool _isLoggedIn;
        private string _title = "Home Planner - Admin";
        //private MainWindowViewType _selectedViewType = MainWindowViewType.Login;

        //public MainWindowViewType SelectedViewType
        //{
        //    get => _selectedViewType;
        //    set => SetProperty(ref _selectedViewType, value);
        //}



        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string? LoggedUserName
        {
            get => _loggedUserName;
            set => SetProperty(ref _loggedUserName, value);
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        public DelegateCommand ViewLoadedCommand { get; }

        public DelegateCommand ShowLoginCommand { get; }

        public DelegateCommand ShowTasksCommand { get; }

        public DelegateCommand LogoutCommand { get; }



        public MainWindowViewModel(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
            //DIHelper.Instance.EventAggregator.GetEvent<OnLoginEvent>().Subscribe(onLoginSucceeded);
            //DIHelper.Instance.EventAggregator.GetEvent<OnLogoutEvent>().Subscribe(logout);

            ShowLoginCommand = new DelegateCommand(showLogin);
            ShowTasksCommand = new DelegateCommand(showTasks, () => IsLoggedIn);
            LogoutCommand = new DelegateCommand(logout, () => IsLoggedIn);
            ViewLoadedCommand = new DelegateCommand(onViewLoaded);

            DIHelper.Instance.SessionService.OnLogin += sessionService_OnLogin;
            DIHelper.Instance.SessionService.OnLogout += sessionService_OnLogout;
        }



        private void sessionService_OnLogout(object? sender, EventArgs e)
        {
            raiseLogout();
        }

        private void sessionService_OnLogin(object? sender, SessionService.LoginInfo e)
        {
            onLoginSucceeded(e.UserName);
        }

        private void showLogin()
        {
            //SelectedViewType = MainWindowViewType.Login;
            showLoginContent();
        }

        private void showLoginContent(bool isLogout = false)
        {
            var parameters = new NavigationParameters
            {
                { "IsLogout", isLogout }
            };
            DIHelper.Instance.RegionManager.RequestNavigate(RegionNames.MainRegion, nameof(LoginControlView), parameters);
        }

        private void showTasks()
        {
            if (!IsLoggedIn)
                return;

            //DIHelper.Instance.EventAggregator.GetEvent<OnTasksNavigateEvent>().Publish();
            //SelectedViewType = MainWindowViewType.Tasks;
            DIHelper.Instance.RegionManager.RequestNavigate(RegionNames.MainRegion, nameof(TasksControlView));
        }

        private void onLoginSucceeded(string userName)
        {
            LoggedUserName = userName;
            IsLoggedIn = true;
            raiseCommandCanExecutes();

            showTasks();
        }

        private void logout()
        {
            raiseLogout();
            showLoginContent(true);
        }

        private void raiseLogout()
        {
            LoggedUserName = null;
            IsLoggedIn = false;
            raiseCommandCanExecutes();
        }

        private void raiseCommandCanExecutes()
        {
            LogoutCommand.RaiseCanExecuteChanged();
            ShowTasksCommand.RaiseCanExecuteChanged();
        }

        private void onViewLoaded()
        {
            showLogin();
        }
    }

    //public enum MainWindowViewType
    //{
    //    Login,
    //    Tasks
    //}
}