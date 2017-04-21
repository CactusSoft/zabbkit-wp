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
    public class HostsPageViewModel : ItemsScreenWithGraphs<Host>
    {
        private readonly IHostProxyServer _hostProxyServer;
        private readonly IAnalyticsService _analyticsService;

        public HostsPageViewModel(IHostProxyServer hostProxyServer, INavigationService navigationService,
            IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler, IAnalyticsService analyticsService)
            : base(busyIndicatorManager, errorHandler, navigationService)
        {
            _hostProxyServer = hostProxyServer;
            _analyticsService = analyticsService;
        }

        public string GroupId
        {
            get;
            set;
        }

        public void NavigateToTriggers(Host host)
        {
            NavigationService.UriFor<HostTriggersPageViewModel>()
                .WithParam(vm => vm.DisplayName, host.Name.ToLowerInvariant())
                .WithParam(vm => vm.HostId, host.Id)
                .Navigate();
        }

        public override void NavigateToGraphs()
        {
            NavigationService
                .UriFor<GraphsPageViewModel>()
                .WithParam(vm => vm.GroupId, GroupId)
                .Navigate();
        }

        public override void NavigateToData()
        {
            NavigationService
                .UriFor<DataPageViewModel>()
                .WithParam(vm => vm.GroupId, GroupId)
                .Navigate();
        }


        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.HostsPage);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;

            //var groupId = GroupId > 0 ? GroupId : (uint?)null;

            try
            {              
                Items = await Executer.Execute(
                    () => _hostProxyServer.GetHostsAsync(GroupId, new[] {HostSortField.ByName}));
            }
            catch (Exception ex)
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
