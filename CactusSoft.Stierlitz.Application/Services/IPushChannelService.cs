using System;

namespace CactusSoft.Stierlitz.Application.Services
{
    public interface IPushChannelService
    {
        Uri Uri { get; }
        void Connect();

        event EventHandler<EventArgs<string>> ManagementIdChanged;
        event EventHandler<EventArgs> Error;
    }
}
