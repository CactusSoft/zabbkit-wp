using System;
using System.Linq;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Localization;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
	public class ServersPageViewModel : Screen
	{
		private readonly INavigationService _navigationService;
	    private readonly IApplicationSettings _applicationSettings;
        private readonly IServerChecker _serverChecker;
	    private readonly IErrorHandler _errorHandler;
	    private readonly IMessagingService _messagingService;
        private readonly IGlobalBusyIndicatorManager _globalBusyIndicatorManager;
	    private readonly IAnalyticsService _analyticsService;
	    private string _oldName;
        private string _name;
        private string _uri = "https://";

        public ServersPageViewModel(INavigationService navigationService, IApplicationSettings applicationSettings,
            IServerChecker serverChecker, IErrorHandler errorHandler, IMessagingService messagingService,
            IGlobalBusyIndicatorManager globalBusyIndicatorManager, IAnalyticsService analyticsService)
		{
			_navigationService = navigationService;
            _applicationSettings = applicationSettings;
            _serverChecker = serverChecker;
            _errorHandler = errorHandler;
            _messagingService = messagingService;
            _globalBusyIndicatorManager = globalBusyIndicatorManager;
            _analyticsService = analyticsService;
		}

        public bool CanSave
	    {
	        get
	        {
	            return !string.IsNullOrWhiteSpace(Name) && Uri != null
	                   && System.Uri.IsWellFormedUriString(Uri, UriKind.Absolute)
                       && !IsBusy;
	        }
	    }

	    public bool CanCancel
	    {
	        get
	        {
	            return !IsBusy;
	        }
	    }

        public bool IsEditing
        {
            get;
            set;
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
                NotifyOfPropertyChange(() => IsBusy);
                NotifyOfPropertyChange(() => CanSave);
                NotifyOfPropertyChange(() => CanCancel);
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        public string Uri
        {
            get
            {
                return _uri;
            }

            set
            {
                _uri = value;
                NotifyOfPropertyChange(() => Uri);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        private bool IsServerExist(string name)
        {
            return _applicationSettings.Servers.Values.Any(server => server.Name == name);
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            if (IsEditing)
            {
                _oldName = Name;
            }
        }

        public async void Save()
        {
            bool isServerValid = await CheckServer();
            if (!isServerValid)
            {
                return;
            }

            if (IsEditing)
            {
                _applicationSettings.Servers.Remove(_oldName);
                _applicationSettings.Servers[Name] = new Server {Name = Name, Uri = Uri};
                _navigationService.GoBack();
            }
            else
            {
                if (!IsServerExist(Name))
                {
                    _applicationSettings.Servers[Name] = new Server {Name = Name, Uri = Uri};
                    _analyticsService.AddServer();
                    _navigationService.GoBack();
                }
                else
                {
                    _messagingService.Alert(AppResources.Attention, AppResources.ServerAlreadyExist);
                }
            }
        }

        public void Cancel()
        {
            _navigationService.GoBack();
        }

        private async Task<bool> CheckServer()
        {
            IsBusy = true;
            try
            {
                bool isValidZabbixServer = await Executer.Execute(() => _serverChecker.CheckUriAsync(Uri));
                IsBusy = false;
                if (!isValidZabbixServer)
                {
                    _messagingService.Alert(AppResources.Attention, AppResources.InvalidZabbixServerUri);
                    
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                _errorHandler.Handle(e);
                IsBusy = false;
                return false;
            }
        }
	}
}