using AutoMapper;
using HPAdmin.Data.DbContext;
using HPAdmin.UI.Controls.MVVM.Views;
using HPAdmin.UI.Helpers;
using HPAdmin.UI.ViewModels.Base;

namespace HPAdmin.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "Home Planner - Admin";
        //private MainWindowViewType _selectedViewType = MainWindowViewType.Login;

        //public MainWindowViewType SelectedViewType
        //{
        //    get => _selectedViewType;
        //    set => SetProperty(ref _selectedViewType, value);
        //}

        public string Title
        {
            get => string.IsNullOrEmpty(LoggedUserName) ? _title : $"{_title} - {LoggedUserName}";
            set => SetProperty(ref _title, value);
        }

        public string? LoggedUserName => DIHelper.Instance.SessionService.UserName;

        public bool IsLoggedIn => DIHelper.Instance.SessionService.IsLoggedIn;

        //public DelegateCommand ViewLoadedCommand { get; }

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
            //ViewLoadedCommand = new DelegateCommand(onViewLoaded);

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
            RaisePropertyChanged(nameof(Title));
        }

        private void showLogin()
        {
            //SelectedViewType = MainWindowViewType.Login;
            showLoginContent(!IsLoggedIn);
        }

        private void showLoginContent(bool isLogout = false)
        {
            DIHelper.Instance.RegionManager.RequestNavigate(RegionNames.MainRegion, nameof(LoginControlView));
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
            raiseCommandCanExecutes();
            showTasks();
        }

        private void logout()
        {
            DIHelper.Instance.SessionService.Logout();
            showLoginContent(true);
            raiseLogout();
        }

        private void raiseLogout()
        {
            raiseCommandCanExecutes();
            RaisePropertyChanged(nameof(Title));
        }

        private void raiseCommandCanExecutes()
        {
            LogoutCommand.RaiseCanExecuteChanged();
            ShowTasksCommand.RaiseCanExecuteChanged();
        }

        //private void onViewLoaded()
        //{
        //    showLogin();
        //}
    }

    //public enum MainWindowViewType
    //{
    //    Login,
    //    Tasks
    //}
}