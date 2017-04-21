namespace CactusSoft.Stierlitz.Application.ViewModels.FavoritesHub
{
    public interface IFavoritesViewModel
    {
        bool NeedUpdateItems
        {
            get;
            set;
        }

        void Update();
    }
}
