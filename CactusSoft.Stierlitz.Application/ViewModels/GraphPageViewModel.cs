using System;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.Messages;
using CactusSoft.Stierlitz.Application.ViewModels.Period;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Facades;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class GraphPageViewModel : Screen, IHandle<PeriodChangedMessage>
    {
        private const int GRAPH_WIDTH = 480 * 1;
        private const int GRAPH_HEIGHT = 710 * 1;
        private static readonly TimeSpan Epsilon = TimeSpan.FromMinutes(5);
        private readonly IGraphsProxyServer _graphsProxyServer;
        private readonly IErrorHandler _errorHandler;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IFavoritesStorage<Graph> _favoritesStorage;
        private readonly IAnalyticsService _analyticsService;
        private byte[] _graphData;
        private bool _isBusy;
        private DateTime? _startTime;
        private Graph _graph;
        private bool _isFirstLoading;

        public GraphPageViewModel(IGraphsProxyServer graphsProxyServer, IErrorHandler errorHandler, INavigationService navigationService, IEventAggregator eventAggregator, IFavoritesStorage<Graph> favoritesStorage, IAnalyticsService analyticsService)
        {
            _graphsProxyServer = graphsProxyServer;
            _errorHandler = errorHandler;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _favoritesStorage = favoritesStorage;
            _analyticsService = analyticsService;

            Period = ZabbixGraphsProxyServer.MinPeriod;
        }

        public string GraphId
        {
            get;
            set;
        }

        public string GraphName
        {
            get;
            set;
        }

        public byte[] GraphData
        {
            get
            {
                return _graphData;
            }
            set
            {
                _graphData = value;
                NotifyOfPropertyChange(() => GraphData);
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
                NotifyOfPropertyChange(() => CanChangePeriod);
                NotifyOfPropertyChange(() => CanNext);
                NotifyOfPropertyChange(() => CanPrevious);
                NotifyOfPropertyChange(() => CanSubscribe);
            }
        }

        public bool CanChangePeriod
        {
            get
            {
                return !IsBusy;
            }
        }

        public bool CanSubscribe
        {
            get
            {
                return !IsBusy;
            }
        }

        public int GraphHeight
        {
            get
            {
                return GRAPH_HEIGHT;
            }
        }

        public bool CanNext
        {
            get
            {
                if (DateTime.Now - StartTime - Epsilon < Period)
                {
                    return false;
                }
                return !IsBusy;
            }
        }

        public bool CanPrevious
        {
            get
            {
                if (StartTime.ToUnixTicks() <= 0)
                {
                    return false;
                }
                return !IsBusy;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return _startTime.HasValue ? _startTime.Value : DateTime.Now - Period;
            }
            set
            {
                _startTime = value;
            }
        }

        public TimeSpan Period
        {
            get;
            set;
        }

        public bool IsSubscribed
        {
            get
            {
                return _favoritesStorage.Exists(Graph);
            }
        }

        public bool IsFirstLoading
        {
            get
            {
                return _isFirstLoading;
            }
            set
            {
                _isFirstLoading = value;
                NotifyOfPropertyChange(() => IsFirstLoading);
            }
        }

        private Graph Graph
        {
            get
            {
                return _graph ?? (_graph = new Graph{GraphId = GraphId, Name = GraphName});
            }
        }

        public void Subscribe()
        {
            if (!IsSubscribed)
            {
                _favoritesStorage.Add(Graph);
                _analyticsService.AddGraphToFavorites();
            }
            else
            {
                _favoritesStorage.Remove(Graph);
            }
            NotifyOfPropertyChange(() => IsSubscribed);
        }

        public void ChangePeriod()
        {
            _navigationService.UriFor<PeriodPageViewModel>().Navigate();
        }

        public void Next()
        {
            NotifyOfPropertyChange(() => CanNext);
            if (!CanNext)
            {
                return;
            }

            DateTime stime = StartTime + Period;

            StartTime = stime;
            LoadImageAsync();
            _analyticsService.NextPeriod();
        }

        public void Previous()
        {
            DateTime stime = StartTime - Period;
            StartTime = stime;
            LoadImageAsync();
            NotifyOfPropertyChange(() => CanPrevious);
            NotifyOfPropertyChange(() => CanNext);
            _analyticsService.PreviousPeriod();
        }


        public void Handle(PeriodChangedMessage message)
        {
            Period = message.Period;
            _startTime = message.StartTime;
            LoadImageAsync();
            _analyticsService.ChangePeriod(Period);
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _eventAggregator.Subscribe(this);
            IsFirstLoading = true;
            LoadImageAsync();
        }

        private async void LoadImageAsync()
        {
            IsBusy = true;
            try
            {
                GraphData = await Executer.Execute(() => _graphsProxyServer.GetGraphImageAsync(GraphId, GRAPH_HEIGHT, GRAPH_WIDTH, StartTime, Period));
            }
            catch (Exception e)
            {
                _errorHandler.Handle(e);
                _navigationService.GoBack();
            }
            finally
            {
                IsBusy = false;
            }

            if (IsFirstLoading)
            {
                IsFirstLoading = false;
            }
        }
    }
}
