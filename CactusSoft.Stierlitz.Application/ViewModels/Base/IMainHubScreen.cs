using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.Base
{
    public interface IMainHubScreen : IScreen, IUpdate
    {
        bool IsBusy
        {
            get;
        }
    }
}
