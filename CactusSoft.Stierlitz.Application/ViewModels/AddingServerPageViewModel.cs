using System;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
	public class AddingServerPageViewModel : Screen
	{
		private readonly INavigationService _navigationService;
	    private readonly IApplicationSettings _applicationSettings;
        private string _name;
        private string _uri;

        public AddingServerPageViewModel(INavigationService navigationService, IApplicationSettings applicationSettings)
		{
			_navigationService = navigationService;
            _applicationSettings = applicationSettings;
		}

	    public bool CanAdd
	    {
	        get
	        {
	            return !string.IsNullOrWhiteSpace(Name) && Uri != null
	                   && !System.Uri.IsWellFormedUriString(Uri, UriKind.Absolute);
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
            }
        }

        public void Add()
        {
            //TODO: check, if name exists
            _applicationSettings.Servers.Add(Name, new Server {Name = Name, Uri = Uri});
            _navigationService.GoBack();
        }
	}
}