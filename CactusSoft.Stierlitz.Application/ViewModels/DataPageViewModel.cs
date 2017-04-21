using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class DataPageViewModel : UpdateItemsScreen<DataItemViewModel>
    {
        private readonly IDataProxyServer _dataProxyServer;
        private readonly IAnalyticsService _analyticsService;
        private readonly IHostProxyServer _hostProxyServer;

        private static IList<Host> _cachedHosts = new List<Host>();

        public DataPageViewModel(IGlobalBusyIndicatorManager busyIndicatorManager, 
            IErrorHandler errorHandler, 
            IDataProxyServer dataProxyServer,
            IHostProxyServer hostProxyServer,
            INavigationService navigationService, 
            IAnalyticsService analyticsService) 
            : base(busyIndicatorManager, errorHandler, navigationService)
        {
            _dataProxyServer = dataProxyServer;
            _analyticsService = analyticsService;
            _hostProxyServer = hostProxyServer;
        }

        public string GroupId { get;  set; }

        public string HostId { get; set; }

        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.DataPage);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;
            //var groupId = GroupId != 0 ? GroupId : (uint?)null;
            //var hostId = HostId != 0 ? HostId : (uint?)null;
            try
            {
                var items = await Executer.Execute(() => 
                    _dataProxyServer.GetItemsAsync(GroupId, HostId));

                Items = await ResolveHosts(items);
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

        private async Task<IEnumerable<DataItemViewModel>> ResolveHosts(IEnumerable<Item> items)
        {
            if (items == null)
                return null;

            var cachedIds = _cachedHosts.Select(i => i.Id).ToList();

            var hostIds = items
                .Where(i => !cachedIds.Contains(i.HostId))
                .Select(i => i.HostId)
                .Distinct();

            // try to find out all new hosts in our cache
            if (hostIds.Any())
            {
                var hosts = await _hostProxyServer.GetHostsAsync(null, null, hostIds.ToArray());
                foreach(var host in hosts)
                    _cachedHosts.Add(host);
            }

            var result = new List<DataItemViewModel>();

            foreach (var item in items)
            {
                var itemViewModel = new DataItemViewModel(item);
                itemViewModel.HostName = _cachedHosts
                    .Where(i => i.Id == item.HostId)
                    .Select(i => i.Name)
                    .SingleOrDefault();
                result.Add(itemViewModel);
            }
            return result;
        }
    }
}
