using System;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class GraphsPageViewModel : UpdateItemsScreen<Graph>
    {
        private readonly IGraphsProxyServer _graphsProxyServer;
        private readonly IAnalyticsService _analyticsService;

        public GraphsPageViewModel(IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler, IGraphsProxyServer graphsProxyServer, INavigationService navigationService, IAnalyticsService analyticsService) 
            : base(busyIndicatorManager, errorHandler, navigationService)
        {
            _graphsProxyServer = graphsProxyServer;
            _analyticsService = analyticsService;
        }

        public string GroupId
        {
            get;
            set;
        }

        public string HostId
        {
            get;
            set;
        }

        public void NavigateToGraph(Graph graph)
        {
            NavigationService
                .UriFor<GraphPageViewModel>()
                .WithParam(vm => vm.GraphId, graph.GraphId)
                .WithParam(vm => vm.GraphName, graph.Name)
                .Navigate();
        }

        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.GraphsPage);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;
            //uint? groupId = GroupId != 0 ? GroupId : (uint?) null;
            //uint? hostId = HostId != 0 ? HostId : (uint?) null;
            try
            {
                Items = await Executer.Execute(() => _graphsProxyServer.GetGraphsAsync(GroupId, HostId));
                
            }
            catch (Exception e)
            {
                ErrorHandler.Handle(e);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
