using System.Windows.Controls;
using AutoMapper;
using HPAdmin.Data.DbContext;
using HPAdmin.UI.Controls.MVVM.ViewModels;
using HPAdmin.UI.Controls.MVVM.Views;
using HPAdmin.UI.Heleprs;
using HPAdmin.UI.ViewModels.Base;

namespace HPAdmin.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _currentView;
        private string? _loggedUserName;
        private bool _isLoggedIn;
        private LoginControlView? _loginView;
        private TasksControlView? _taskView;
        private string _title = "Home Planner - Admin";

        public UserControl CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }
        
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
            ShowLoginCommand = new DelegateCommand(showLogin);
            ShowTasksCommand = new DelegateCommand(showTasks, () => IsLoggedIn);
            LogoutCommand = new DelegateCommand(logout, () => IsLoggedIn);
            ViewLoadedCommand = new DelegateCommand(onViewLoaded);

        }

        

        private void showLogin()
        {
            if (_loginView == null)
            {
                _loginView = DIHelper.Instance.Resolve<LoginControlView>();
                if (_loginView.DataContext is LoginControlViewModel loginVm)
                {
                    loginVm.LoginSucceeded += onLoginSucceeded;
                    loginVm.LogoutRequested += logout;
                }
            }

            CurrentView = _loginView;
        }

        private void showTasks()
        {
            if (!IsLoggedIn)
                return;

            if (_taskView == null)
            {
                _taskView = DIHelper.Instance.Resolve<TasksControlView>();
                if (_taskView.DataContext is TasksControlViewModel taskVm)
                {
                    taskVm.LoadData();
                }
            }

            CurrentView = _taskView;
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
            //todo: tady chybi poslani informace do logincontrol ze se odhlasuju, melo by byt take reseno pres session a eventagregator nebo podobne,
            //ted se na talcitko z mainwindow nerfreshe ui, jen kdyz to udelam primo z logincontrol
            LoggedUserName = null;
            IsLoggedIn = false;
            _taskView = null;

            raiseCommandCanExecutes();
            showLogin();
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
}