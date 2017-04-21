using Caliburn.Micro;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;

namespace CactusSoft.Stierlitz.Application.ViewModels.Base
{
    public abstract class TriggersScreen : UpdateItemsScreen<Trigger>
    {
        protected readonly ITriggerProxyServer TriggerProxyServer;

        protected TriggersScreen(ITriggerProxyServer triggerProxyServer, INavigationService navigationService,
                                 IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler)
            : base(busyIndicatorManager, errorHandler, navigationService)
        {
            TriggerProxyServer = triggerProxyServer;
        }

        public void NavigateToEvents(Trigger trigger)
        {
            NavigationService.UriFor<EventsPageViewModel>()
                .WithParam(vm => vm.TriggerId, trigger.Id)
                .WithParam(vm => vm.Description, trigger.Description)
                .WithParam(vm => vm.TriggerPriority, trigger.Priority)
                .Navigate();
        }
    }
}
