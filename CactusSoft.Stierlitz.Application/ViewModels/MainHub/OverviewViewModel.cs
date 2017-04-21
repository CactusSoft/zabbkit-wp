using System;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using Caliburn.Micro;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Localization;

namespace CactusSoft.Stierlitz.Application.ViewModels.MainHub
{
    public class OverviewViewModel : UpdateItemsScreen<HostGroup>, IMainHubScreen
    {
        private const int DEFAULT_ITEMS_COUNT = 5;
        private readonly IHostGroupProxyServer _hostGroupProxyServer;
        private readonly IAnalyticsService _analyticsService;
        private bool _isBusy;

        public OverviewViewModel(IHostGroupProxyServer hostGroupProxyServer, INavigationService navigationService, 
            IErrorHandler errorHandler, IAnalyticsService analyticsService)
            : base(null, errorHandler, navigationService)
        {
            _hostGroupProxyServer = hostGroupProxyServer;
            _analyticsService = analyticsService;
        }

        public override string DisplayName
        {
            get
            {
                return AppResources.OverviewTitle;
            }
        }

        public override bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public void NavigateToHosts(HostGroup hostGroup)
        {
            NavigationService
                .UriFor<HostsPageViewModel>()
                .WithParam(vm => vm.DisplayName, hostGroup.Name)
                .WithParam(vm => vm.GroupId, hostGroup.Id)
                .Navigate();
        }

        public void NavigateToAllHostGroups()
        {
            NavigationService.UriFor<HostGroupsPageViewModel>().Navigate();
        }

        public void NavigateToGraphs()
        {
            NavigationService.UriFor<GraphsPageViewModel>().Navigate();
        }

        public void NavigateToData()
        {
            NavigationService.UriFor<DataPageViewModel>().Navigate();
        }


        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.OverviewView);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;

            try
            {
                Items = await Executer.Execute(
                    () => _hostGroupProxyServer.GetHostGroups(new[] {HostGroupsSortField.ByName}, DEFAULT_ITEMS_COUNT));
                
            }
            catch(Exception ex)
            {
                ErrorHandler.Handle(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
	}
}
