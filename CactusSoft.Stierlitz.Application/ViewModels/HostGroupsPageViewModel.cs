using System;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class HostGroupsPageViewModel : ItemsScreenWithGraphs<HostGroup>
	{
        private readonly IHostGroupProxyServer _hostGroupProxyServer;
        private readonly IAnalyticsService _analyticsService;

        public HostGroupsPageViewModel(IHostGroupProxyServer hostGroupProxyServer, INavigationService navigationService,
            IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler, IAnalyticsService analyticsService)
            : base(busyIndicatorManager, errorHandler, navigationService)
        {
            _hostGroupProxyServer = hostGroupProxyServer;
            _analyticsService = analyticsService;
        }

        public void NavigateToHosts(HostGroup hostGroup)
        {
            NavigationService
                .UriFor<HostsPageViewModel>()
                .WithParam(vm => vm.DisplayName, hostGroup.Name.ToLowerInvariant())
                .WithParam(vm => vm.GroupId, hostGroup.Id)
                .Navigate();
        }

        public override void NavigateToGraphs()
        {
            NavigationService.UriFor<GraphsPageViewModel>().Navigate();
        }

        public override void NavigateToData()
        {
            NavigationService.UriFor<DataPageViewModel>().Navigate();
        }

        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.AllHostGroupsPage);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;

            try
            {
                Items = await Executer.Execute(
                    () => _hostGroupProxyServer.GetHostGroups(new[] {HostGroupsSortField.ByName}, null));
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
