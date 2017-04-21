using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers.Base
{
	public abstract class ScreenViewModelStorageHandler<TScreen> : StorageHandler<TScreen> where TScreen : Screen
	{
		public override void Configure()
		{
            Id(vm => vm.DisplayName);
		}
	}
}