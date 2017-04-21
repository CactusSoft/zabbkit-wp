using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers.Base
{
	public abstract class ConductorViewModelStorageHandler<TConductor, TChild> : StorageHandler<TConductor> 
        where TConductor : Conductor<TChild>
	{
		public override void Configure()
		{
			this.ActiveItemIndex()
				.InPhoneState()
				.RestoreAfterViewLoad();
		}
	}
}