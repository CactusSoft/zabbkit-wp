using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class HostTriggersPageViewModel : TriggersScreen
    {
        private readonly IAnalyticsService _analyticsService;

        public HostTriggersPageViewModel(ITriggerProxyServer triggerProxyServer, INavigationService navigationService, 
                                         IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler, IAnalyticsService analyticsService)
            : base(triggerProxyServer, navigationService, busyIndicatorManager, errorHandler)
        {
            _analyticsService = analyticsService;
        }

        public string HostId
        {
            get;
            set;
        }

        public override bool IsBusy
        {
            set
            {
                base.IsBusy = value;
                NotifyOfPropertyChange(() => CanNavigateToGraphs);
                NotifyOfPropertyChange(() => CanNavigateToData);
            }
        }

        public bool CanNavigateToGraphs
        {
            get
            {
                return !IsBusy;
            }
        }

        public void NavigateToGraphs()
        {
            NavigationService
                .UriFor<GraphsPageViewModel>()
                .WithParam(vm => vm.HostId, HostId)
                .Navigate();
        }

        public bool CanNavigateToData
        {
            get
            {
                return !IsBusy;
            }
        }

        public void NavigateToData()
        {
            NavigationService
                .UriFor<DataPageViewModel>()
                .WithParam(vm => vm.HostId, HostId)
                .Navigate();
        }


        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.HostTriggersPage);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;

            //uint? hostId = HostId > 0 ? HostId : (uint?)null;
            IEnumerable<Trigger> triggers;

            try
            {
                triggers = await Executer.Execute(() => TriggerProxyServer.GetTriggers(HostId, null,
                                                                                       null , Select.None));
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex);
                return;
            }
            finally
            {
                IsBusy = false;
            }

            Items = triggers.OrderBy(trigger => trigger.IsOk)
                .ThenByDescending(trigger => trigger.Priority)
                .ThenBy(trigger => trigger.Description)
                .ToList();
        }
    }
}
