using System;
using System.ComponentModel;
using Caliburn.Micro;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Application.ViewModels.MainHub;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Services.Facades;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class MainPageViewModel : Conductor<IMainHubScreen>.Collection.OneActive
    {
        private readonly IUserManagmentFacade _userManagmentFacade;
        private readonly IApplicationSettings _applicationSettings;
        private readonly INavigationService _navigationService;
        private readonly IGlobalBusyIndicatorManager _globalBusyIndicatorManager;
        private readonly IErrorHandler _errorHandler;
        private readonly OverviewViewModel _overviewViewModel;
        private readonly IMessagingService _messagingService;
        private readonly IAnalyticsService _analyticsService;

        public MainPageViewModel(IUserManagmentFacade userManagmentFacade, IApplicationSettings applicationSettings, INavigationService navigationService, 
            IGlobalBusyIndicatorManager globalBusyIndicatorManager, IErrorHandler errorHandler, TriggersViewModel triggersViewModel,
            TimelineViewModel timelineViewModel, OverviewViewModel overviewViewModel, FavoritesViewModel favoritesViewModel, IMessagingService messagingService, IAnalyticsService analyticsService)
		{
            _userManagmentFacade = userManagmentFacade;
            _applicationSettings = applicationSettings;
            _navigationService = navigationService;
            _globalBusyIndicatorManager = globalBusyIndicatorManager;
            _errorHandler = errorHandler;
            _overviewViewModel = overviewViewModel;
            _messagingService = messagingService;
            _analyticsService = analyticsService;

            Items.Add(triggersViewModel);
            Items.Add(_overviewViewModel);
            Items.Add(timelineViewModel);      
            Items.Add(favoritesViewModel);
		}

        public string TriggerIdFromToast { get; set; }
        public bool IsDisabledToastNavigation { get; set; }

        public bool IsBusy
        {
            get
            {
                return _globalBusyIndicatorManager.IsBusy;
            }
            set
            {
                if (value == _globalBusyIndicatorManager.IsBusy)
                {
                    return;
                }

                _globalBusyIndicatorManager.IsBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
                NotifyOfPropertyChange(() => CanUpdate);
                NotifyOfPropertyChange(() => CanLogout);             
                NotifyOfPropertyChange(() => CanNavigateToGraphs);
            }
        }

        public bool CanUpdate
        {
            get
            {
                return !IsBusy;
            }
        }

	    public bool CanLogout
	    {
	        get
	        {
	            return !IsBusy;
	        }
	    }

        public bool CanNavigateToGraphs
        {
            get
            {
                return !IsBusy;
            }
        }

	    public void NavigateToAbout()
		{
			_navigationService.UriFor<AboutPageViewModel>().Navigate();
		}

        public void NavigateToPushSettings()
        {
            _navigationService.UriFor<PushSettingsPageViewModel>().Navigate();
        }

	    public async void Logout()
	    {
            IsBusy = true;

            try
            {
                await Executer.Execute(() => _userManagmentFacade.LogoutAsync());
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _errorHandler.Handle(ex);
                return;
            }

            _applicationSettings.RememberMe = false;
            _applicationSettings.UserCredentials = null;

            _analyticsService.Logout();

            _navigationService.UriFor<LoginPageViewModel>().Navigate();
        }

        public void Update()
        {
            ActiveItem.Update();
        }

        public void NavigateToGraphs()
        {
            _overviewViewModel.NavigateToGraphs();
        }

        public void NavigateToData()
        {
            _overviewViewModel.NavigateToData();
        }


        protected override void OnViewLoaded(object view)
        {
            if (!string.IsNullOrEmpty(TriggerIdFromToast) && !IsDisabledToastNavigation)
            {
                IsDisabledToastNavigation = true;
                _navigationService.UriFor<EventsPageViewModel>()
                    .WithParam(vm => vm.TriggerId, TriggerIdFromToast)
                    .Navigate();
                return;
            }

            base.OnViewLoaded(view);
            _messagingService.RemindToRateApp();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (Screen screen in Items)
            {
                screen.PropertyChanged -= OnPropertyChanged;
                screen.PropertyChanged += OnPropertyChanged;
            }
        }

        protected override void OnDeactivate(bool close)
        {
 	        base.OnDeactivate(close);

            foreach (Screen screen in Items)
            {
                screen.PropertyChanged -= OnPropertyChanged;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != "IsBusy")
            {
                return;
            }
            if (!ActiveItem.Equals(sender))
            {
                return;
            }
            IsBusy = ActiveItem.IsBusy;
        }

        public override void ActivateItem(IMainHubScreen item)
        {
            base.ActivateItem(item);
            IsBusy = item.IsBusy;
        }
    }
}
