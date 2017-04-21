using System.Linq;
using CactusSoft.Stierlitz.Application.Messages;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
	public class ServerPickerPageViewModel : Screen
	{
		private readonly INavigationService _navigationService;
	    private readonly IApplicationSettings _applicationSettings;
	    private readonly IEventAggregator _eventAggregator;
	    private IObservableCollection<Server> _servers = new BindableCollection<Server>();

        public ServerPickerPageViewModel(INavigationService navigationService, IApplicationSettings applicationSettings,
                                         IEventAggregator eventAggregator)
		{
			_navigationService = navigationService;
            _applicationSettings = applicationSettings;
            _eventAggregator = eventAggregator;
		}

        public IObservableCollection<Server> Servers
        {
            get
            {
                return _servers;
            }

            set
            {
                _servers = value;
                NotifyOfPropertyChange(() => Servers);
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            if ((_applicationSettings.Servers.Values.Any()))
            {
                Servers = new BindableCollection<Server>(_applicationSettings.Servers.Values);
            }
        }

        public void Select(Server server)
        {
            _eventAggregator.Publish(new ServerSelectedMessage() { Server = server });
            _navigationService.GoBack();
        }

        public void Edit(Server server)
        {
            _navigationService.UriFor<ServersPageViewModel>()
                .WithParam(x => x.DisplayName, server.Name.ToLowerInvariant())
                .WithParam(x => x.Name, server.Name)
                .WithParam(x => x.Uri, server.Uri)
                .WithParam(x => x.IsEditing, true)
                .Navigate();
        }

        public void Remove(Server server)
        {
            Servers.Remove(server);
            _eventAggregator.Publish(new ServerDeletedMessage() { Server = server });
        }

        public void Add()
        {
            _navigationService.UriFor<ServersPageViewModel>()
                .WithParam(x => x.DisplayName, AppResources.NewServerTitle)
                .Navigate();
        }
	}
}