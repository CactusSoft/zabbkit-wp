using System;
using System.Linq;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.Messages;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Localization;
using CactusSoft.Stierlitz.Services.Facades;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class LoginPageViewModel : Screen, IHandle<ServerSelectedMessage>, IHandle<ServerDeletedMessage>
    {
        private readonly INavigationService _navigationService;
        private readonly IUserManagmentFacade _userManagmentFacade;
        private readonly IGlobalBusyIndicatorManager _globalBusyIndicatorManager;
        private readonly IApplicationSettings _applicationSettings;
        private readonly IEventAggregator _eventAggregator;
        private readonly IErrorHandler _errorHandler;
        private readonly IErrorReporter _errorReporter;
        private readonly IMessagingService _messagingService;
        private readonly IAnalyticsService _analyticsService;
        private string _selectedServerName;
        private string _username;
        private string _password;
        private bool _validatingSession;
        private bool _rememberMe;

        //private readonly DateTime _newServerDate = new DateTime(2013, 10, 1);

        public LoginPageViewModel(IUserManagmentFacade userManagmentFacade, INavigationService navigationService, 
                                  IGlobalBusyIndicatorManager globalBusyIndicatorManager, IApplicationSettings applicationSettings,
                                  IEventAggregator eventAggregator, IErrorHandler errorHandler, IErrorReporter errorReporter, 
                                  IMessagingService messagingService, IAnalyticsService analyticsService)
        {
            _userManagmentFacade = userManagmentFacade;
            _navigationService = navigationService;
            _globalBusyIndicatorManager = globalBusyIndicatorManager;
            _applicationSettings = applicationSettings;
            _eventAggregator = eventAggregator;
            _errorHandler = errorHandler;     
            _errorReporter = errorReporter;
            _messagingService = messagingService;
            _analyticsService = analyticsService;
            _navigationService = navigationService;
            _userManagmentFacade = userManagmentFacade;

#if DEBUG
            // TODO: Remove this code
            if (!_applicationSettings.Servers.Any())
            {
                _applicationSettings.Servers.Add("InsideCactusSoft", new Server { Name = "InsideCactusSoft", Uri = "http://192.168.10.38/zabbix" });
                _applicationSettings.Servers.Add("CactusSoft", new Server { Name = "CactusSoft", Uri = "https://zabbix.inside.cactussoft.biz" });
                _applicationSettings.Servers.Add("techmaster.com", new Server { Name = "techmaster.com", Uri = "http://zabbix.techmaster.com.br/zabbix" });
            }
#endif

            Initialize();
        }

        public string TriggerIdFromToast { get; set; }

        public bool CanLogin
        {
            get
            {
                return _applicationSettings.Servers.Any() &&
                       !string.IsNullOrWhiteSpace(Username) && 
                       !string.IsNullOrWhiteSpace(Password) &&
                       !IsBusy;
            }
        }
        
        public bool IsBusy
        {
            get
            {
                return _globalBusyIndicatorManager.IsBusy;
            }

            set
            {
                _globalBusyIndicatorManager.IsBusy = value;
                NotifyOfPropertyChange(() => CanLogin);
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public string SelectedServerName
        {
            get
            {
                return _selectedServerName;
            }

            set
            {
                _selectedServerName = value;
                NotifyOfPropertyChange(() => SelectedServerName);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool RememberMe
        {
            get
            {
                return _rememberMe;
            }
            set
            {
                _rememberMe = value;
                NotifyOfPropertyChange(() => RememberMe);
            }
        }

        public bool ValidatingSession
        {
            get
            {
                return _validatingSession;
            }

            set
            {
                _validatingSession = value;
                NotifyOfPropertyChange(() => ValidatingSession);
            }
        }

        public void Login()
        {
            var userCredentials = new UserCredentials
            {
                Username = Username,
                Password = Password,
                ServerUri = _applicationSettings.Servers[SelectedServerName].Uri
            };

            LoginAsync(userCredentials);
        }

        public void NavigateToServerPickerPage()
        {
            _navigationService.UriFor<ServerPickerPageViewModel>().Navigate();
        }

        public void NavigateToAbout()
        {
            _navigationService.UriFor<AboutPageViewModel>().Navigate();
        }

        public void Handle(ServerSelectedMessage message)
        {
            SelectedServerName = message.Server.Name;
        }

        public void Handle(ServerDeletedMessage message)
        {
            _applicationSettings.Servers.Remove(message.Server.Name);
            _applicationSettings.UserNames.Remove(message.Server.Name);

            SelectedServerName = GetServerName();

            NotifyOfPropertyChange(() => CanLogin);
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            CheckForErrors();
            //CheckForDate();

            if (ValidatingSession)
            {
                UserCredentials userCredentials = _applicationSettings.UserCredentials;
                LoginAsync(userCredentials);
            }
            
            _eventAggregator.Subscribe(this);
        }

        private void Initialize()
        {
            SelectedServerName = GetServerName();

            string userName;
            _applicationSettings.UserNames.TryGetValue(SelectedServerName, out userName);

            Username = userName;
            RememberMe = _applicationSettings.RememberMe;
            ValidatingSession = RememberMe;
        }


        //private void CheckForDate()
        //{
        //    if (DateTime.Now >= _newServerDate)
        //        _messagingService.Alert(AppResources.Attention, AppResources.OldDateVersionMessage);
        //}

        private void CheckForErrors()
        {
            if (!_errorReporter.HasReports())
            {
                return;
            }

            if (!_messagingService.Message(AppResources.Attention, AppResources.ErrorReportMessage))
            {
                _errorReporter.ClearReports();
                return;
            }

            _errorReporter.SendReport();
        }

        private void UpdateApplicationSettings(UserCredentials userCredentials)
        {
            _applicationSettings.UserCredentials = RememberMe ? userCredentials : null;
            _applicationSettings.LastSelectedServerName = SelectedServerName;
            _applicationSettings.UserNames[_applicationSettings.LastSelectedServerName] = Username;
            _applicationSettings.RememberMe = RememberMe;
        }

        private async void LoginAsync(UserCredentials userCredentials)
        {
            IsBusy = !ValidatingSession;

            try
            {
                await Executer.Execute(() => _userManagmentFacade.LoginAsync(userCredentials));
            }
            catch(Exception ex)
            {
                ValidatingSession = false;
                IsBusy = false;

                Username = userCredentials.Username;
                Password = userCredentials.Password;

                _errorHandler.Handle(ex);
                return;
            }

            UpdateApplicationSettings(userCredentials);
            _eventAggregator.Unsubscribe(this);

            _analyticsService.Login(RememberMe);

            _navigationService.UriFor<MainPageViewModel>()
                .WithParam(vm => vm.TriggerIdFromToast, TriggerIdFromToast)
                .Navigate();

            IsBusy = false;
        }

        private string GetServerName()
        {
            if (!_applicationSettings.Servers.Any())
            {
                return AppResources.AddServer;
            }
            var lastSelectedServerName = _applicationSettings.LastSelectedServerName;
            if (lastSelectedServerName != null)
            {
                if (_applicationSettings.Servers.ContainsKey(lastSelectedServerName))
                {
                    return lastSelectedServerName;
                }
                return _applicationSettings.Servers.Keys.First();
            }
            return _applicationSettings.Servers.Keys.First();
        }
    }
}
