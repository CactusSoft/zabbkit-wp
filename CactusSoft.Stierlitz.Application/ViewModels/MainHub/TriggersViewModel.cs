using System;
using System.Linq;
using System.Collections.Generic;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Localization;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.MainHub
{
    public class TriggersViewModel : TriggersScreen, IMainHubScreen
    {
        private readonly IAnalyticsService _analyticsService;
        private const int DEFAULT_ITEMS_COUNT = 5;
        private bool _isBusy;

        public TriggersViewModel(ITriggerProxyServer triggerProxyServer, INavigationService navigationService, 
                                        IErrorHandler errorHandler, IAnalyticsService analyticsService)
            : base(triggerProxyServer, navigationService, null, errorHandler)
        {
            _analyticsService = analyticsService;
        }

        public override string DisplayName
        {
            get
            {
                return AppResources.TriggersTitle;
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

        public void NavigateToTriggers()
        {
            NavigationService
                .UriFor<TriggersPageViewModel>()
                .Navigate();
        }

        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.TriggersView);
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
                .Take(DEFAULT_ITEMS_COUNT)
                .ToList();
           
        }
	}
}
