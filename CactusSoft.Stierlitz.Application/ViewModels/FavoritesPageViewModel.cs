using System.Linq;
using CactusSoft.Stierlitz.Application.ViewModels.FavoritesHub;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class FavoritesPageViewModel : Conductor<IFavoritesViewModel>.Collection.OneActive
    {
        private bool _isNew;
        public FavoritesPageViewModel(TriggersViewModel triggersViewModel, GraphsViewModel graphsPageViewModel)
        {
            triggersViewModel.NeedUpdateItems = true;
            triggersViewModel.NeedUpdateItems = true;
            Items.Add(triggersViewModel);
            Items.Add(graphsPageViewModel);
            _isNew = true;
        }

        public string SelectedItemType
        {
            get;
            set;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var item in Items)
            {
                item.NeedUpdateItems = !_isNew;
            }
            _isNew = false;

            if (SelectedItemType == null)
            {
                return;
            }

            var vm = Items.SingleOrDefault(item => item.GetType().ToString().Equals(SelectedItemType));
            if (vm == null)
            {
                return;
            }
            ActivateItem(vm);
            vm.Update();
        }
    }
}
