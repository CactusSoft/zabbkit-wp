using System;
using System.Collections.Generic;
using System.Linq;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;
using Caliburn.Micro;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class TriggersPageViewModel : TriggersScreen
    {
        private readonly IAnalyticsService _analyticsService;

        public TriggersPageViewModel(ITriggerProxyServer triggerProxyServer, INavigationService navigationService,
            IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler, IAnalyticsService analyticsService)
            : base(triggerProxyServer, navigationService, busyIndicatorManager, errorHandler)
        {
            _analyticsService = analyticsService;
        }

        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.AllTriggersPage);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;

            IEnumerable<Trigger> triggers;

            try
            {
                triggers = await Executer.Execute(() => TriggerProxyServer.GetTriggers(null, null,
                                                                                       null, Select.Extend));
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
