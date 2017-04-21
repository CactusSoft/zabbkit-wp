using System;
using System.Reflection;
using System.Windows;
using CactusSoft.Stierlitz.Application.ViewModels.Period;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Analitics;
using CactusSoft.Stierlitz.Services.Web;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;
using Caliburn.Micro;
using CactusSoft.Stierlitz.Application.ViewModels.MainHub;
using CactusSoft.Stierlitz.Application.Services;
using CactusSoft.Stierlitz.Services.Web.Configurations;
using CactusSoft.Stierlitz.Services.Facades;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.Settings;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using CactusSoft.Stierlitz.Services.Web.RequestBodies;
using CactusSoft.Stierlitz.Services.Web.WebChannel;
using CactusSoft.Stierlitz.Application.ViewModels;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application
{
	public partial class Bootstrapper
	{
		partial void RegisterCustomObjects(PhoneContainer container)
		{
            RegisterServices(container);
		    RegisterViewModels(container);
		}

        private void RegisterServices(PhoneContainer container)
        {
            container.RegisterHandler(typeof(IGlobalBusyIndicatorManager), string.Empty,
                c => GlobalBusyIndicatorManager.Create((PhoneApplicationPage)RootFrame.Content));

            container.Singleton<IServiceConfiguration, ServiceConfiguration>();
            container.Singleton<IWebConfiguration, WebConfiguration>();
            container.PerRequest<IApplicationConfiguration, ApplicationConfiguration>();

            container.PerRequest<IRequestBodyBuilder, RequestBodyBuilder>();
            container.PerRequest<IWebChannel, ZabbixWebChannel>();

            container.PerRequest<IUserProxyServer, ZabbixUserProxyServer>();
            container.PerRequest<IUserManagmentFacade, UserManagmentFacade>();
            container.PerRequest<IHostGroupProxyServer, ZabbixHostGroupProxyServer>();
            container.PerRequest<IHostProxyServer, ZabbixHostProxyServer>();
            container.PerRequest<ITriggerProxyServer, ZabbixTriggerProxyServer>();
            container.PerRequest<IEventProxyServer, ZabbixEventProxyServer>();
            container.PerRequest<IServerChecker, ZabbixServerChecker>();
            container.PerRequest<IGraphsProxyServer, ZabbixGraphsProxyServer>();
            container.PerRequest<IDataProxyServer, ZabbixDataProxyServer>();

            container.PerRequest<IDeviceManagementService, DeviceManagementService>();
            container.Singleton<IPushChannelService, PushChannelService>();

            container.Singleton<IApplicationSettings, ApplicationSettings>();
            container.PerRequest<ISettingsStorage, SettingsStorage>();
            container.PerRequest<IErrorHandler, ErrorHandler>();
            container.PerRequest<IErrorReporter, ErrorReporter>();
            container.PerRequest<IMessagingService, MessagingService>();            
            container.PerRequest<IDeviceInformationManager, DeviceInformationManager>();
            container.PerRequest<INetworkStateManager, NetworkStateManager>();

            container.Singleton<IFavoritesStorage<Trigger>, FavoritesStorage<Trigger>>();
            container.Singleton<IFavoritesStorage<Graph>, FavoritesStorage<Graph>>();
            container.PerRequest<IIsolatedStorageFactory, IsolatedStorageFactory>();

            container.Singleton<IAnalyticsService, FlurryAnalytics>();

            var navigationServiceResolver = new NavigationServiceResolver(RootFrame);
            container.RegisterInstance(typeof(INavigationServiceResolver), null, navigationServiceResolver);

            container.Singleton<AnaliticsNavigationPageLogger>();
            container.GetInstance(typeof (AnaliticsNavigationPageLogger), null);

        }

        private static void RegisterViewModels(PhoneContainer container)
        {
            container.PerRequest<LoginPageViewModel>();
            container.PerRequest<ServerPickerPageViewModel>();
            container.PerRequest<ServersPageViewModel>();
            container.PerRequest<MainPageViewModel>();

            container.PerRequest<OverviewViewModel>();
            container.PerRequest<HostGroupsPageViewModel>();
            container.PerRequest<HostsPageViewModel>();
            container.PerRequest<HostTriggersPageViewModel>();
            container.PerRequest<EventsPageViewModel>();

            container.PerRequest<TimelineViewModel>();
            container.PerRequest<TimelinePageViewModel>();

            container.PerRequest<TriggersViewModel>();
            container.PerRequest<TriggersPageViewModel>();
                       
            container.PerRequest<AboutPageViewModel>();
            container.Singleton<PushSettingsPageViewModel>();
            container.PerRequest<GraphsPageViewModel>();
            container.PerRequest<GraphPageViewModel>();
            container.PerRequest<DataPageViewModel>();
            container.PerRequest<PeriodPageViewModel>();
            container.PerRequest<CustomPeriodPageViewModel>();

            container.PerRequest<FavoritesViewModel>();
            container.PerRequest<FavoritesPageViewModel>();
            container.PerRequest<ViewModels.FavoritesHub.TriggersViewModel>();
            container.PerRequest<ViewModels.FavoritesHub.GraphsViewModel>();
        }

	    partial void AddCustomConventions()
		{
			AddGenericItemsControlConvention<Pivot>(
				() => Pivot.HeaderTemplateProperty,
				() => Pivot.SelectedItemProperty);

			AddGenericItemsControlConvention<Panorama>(
				() => Panorama.HeaderTemplateProperty,
				() => Panorama.SelectedItemProperty);
		}

        protected override void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            var errorReporter = IoC.Get<IErrorReporter>();
            errorReporter.WriteReport(e.ExceptionObject.ToString());
            var analitics = IoC.Get<IAnalyticsService>();
            analitics.UnhandledException(e.ExceptionObject);

            base.OnUnhandledException(sender, e);
        }

        protected override void OnActivate(object sender, ActivatedEventArgs e)
        {
            base.OnActivate(sender, e);

            var serviceResolver = IoC.Get<INavigationServiceResolver>();
            if (!e.IsApplicationInstancePreserved)
            {
                serviceResolver.TryRestore();
            }

            var analitics = IoC.Get<IAnalyticsService>();
            analitics.StartSession();

            serviceResolver.TryResolve();

            ApplicationUsageHelper.OnApplicationActivated();
        }

        protected override void OnDeactivate(object sender, DeactivatedEventArgs e)
        {
            base.OnDeactivate(sender, e);
            var serviceResolver = IoC.Get<INavigationServiceResolver>();
            serviceResolver.Preserve();
        }

        protected override void OnLaunch(object sender, LaunchingEventArgs e)
        {
            base.OnLaunch(sender, e);
            var serviceResolver = IoC.Get<INavigationServiceResolver>();
            serviceResolver.TryRestore();
            
            ApplicationUsageHelper.Init(Assembly.GetExecutingAssembly().GetVersion());

            var analitics = IoC.Get<IAnalyticsService>();
            analitics.StartSession();

            // register at MSPN
            var pushChannelService = IoC.Get<IPushChannelService>();
            var vm = IoC.Get<PushSettingsPageViewModel>();
            pushChannelService.Error += (o, args) =>
                Deployment.Current.Dispatcher.BeginInvoke(() => { vm.IsError = true; });
            pushChannelService.ManagementIdChanged += (o, args) => 
                Deployment.Current.Dispatcher.BeginInvoke(() => { vm.Token = args.Data; });
            pushChannelService.Connect();
        }

        protected override void OnClose(object sender, ClosingEventArgs e)
        {
            base.OnClose(sender, e);

            var analitics = IoC.Get<IAnalyticsService>();
            analitics.EndSession();
        }
	}
}