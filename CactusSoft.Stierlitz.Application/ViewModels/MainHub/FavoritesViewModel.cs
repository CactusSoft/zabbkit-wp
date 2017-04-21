using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Localization;
using CactusSoft.Stierlitz.Services.Facades;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.MainHub
{
    public class FavoritesViewModel : Screen, IMainHubScreen
    {
        private readonly IFavoritesStorage<Trigger> _triggerFavoritesStorage;
        private readonly IFavoritesStorage<Graph> _graphsFavoritesStorage;
        private readonly INavigationService _navigationService;

        public FavoritesViewModel(IFavoritesStorage<Trigger> triggerFavoritesStorage, IFavoritesStorage<Graph> graphsFavoritesStorage, INavigationService navigationService)
        {
            _triggerFavoritesStorage = triggerFavoritesStorage;
            _graphsFavoritesStorage = graphsFavoritesStorage;
            _navigationService = navigationService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            NotifyOfPropertyChange(() => CanNavigateToFavoritesTriggers);
            NotifyOfPropertyChange(() => CanNavigateToFavoritesGraphs);
        }

        public bool CanNavigateToFavoritesTriggers
        {
            get
            {
                return _triggerFavoritesStorage.Any();
            }
        }

        public bool CanNavigateToFavoritesGraphs
        {
            get
            {
                return _graphsFavoritesStorage.Any();
            }
        }

        public void NavigateToFavoritesTriggers()
        {
            _navigationService
                .UriFor<FavoritesPageViewModel>()
                .WithParam(vm => vm.SelectedItemType, typeof(FavoritesHub.TriggersViewModel).ToString())
                .Navigate();
        }

        public void NavigateToFavoritesGraphs()
        {
            _navigationService
                .UriFor<FavoritesPageViewModel>()
                .WithParam(vm => vm.SelectedItemType, typeof(FavoritesHub.GraphsViewModel).ToString())
                .Navigate();
        }

        public override string DisplayName
        {
            get
            {
                return AppResources.Favorites;
            }
        }


        #region Implementation IMainHubScreen
        
        public void Update()
        {
            //do nothing
        }

        public bool IsBusy
        {
            get
            {
                return false;
            }
        }

        #endregion
    }
}
