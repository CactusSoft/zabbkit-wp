using System.Collections.Generic;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Localization;
using CactusSoft.Stierlitz.Services.Facades;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.FavoritesHub
{
    public class GraphsViewModel : Screen, IFavoritesViewModel
    {
        private readonly IFavoritesStorage<Graph> _favoritesStorage;
        private readonly INavigationService _navigationService;

        public GraphsViewModel(IFavoritesStorage<Graph> favoritesStorage, INavigationService navigationService)
        {
            _favoritesStorage = favoritesStorage;
            _navigationService = navigationService;
        }

        public override string DisplayName
        {
            get
            {
                return AppResources.Graphs;
            }
        }

        public IList<Graph> Items
        {
            get
            {
                return _favoritesStorage.Favorites();
            }
        }

        public bool NeedUpdateItems
        {
            get;
            set;
        }

        public void Update()
        {
            if (NeedUpdateItems)
            {
                NotifyOfPropertyChange(() => Items);
            }
            NeedUpdateItems = false;
        }

        public void NavigateToGraph(Graph graph)
        {
            _navigationService
                .UriFor<GraphPageViewModel>()
                .WithParam(vm => vm.GraphId, graph.GraphId)
                .WithParam(vm => vm.GraphName, graph.Name)
                .Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Update();
        }

    }
}
