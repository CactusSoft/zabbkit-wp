using System;
using System.Net;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Localization;
using CactusSoft.Stierlitz.Services.Web.Exceptions;

namespace CactusSoft.Stierlitz.Services
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly IMessagingService _messagingService;

        public ErrorHandler(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public void Handle(Exception exception)
        {
            if (exception is WebServiceException)
            {
                ShowMessage(AppResources.Error, AppResources.ServerCommunicationErrorHasOccured);
            }
            else if (exception is WebException)
            {
                ShowMessage(AppResources.Error, AppResources.NetworkConnectionProblemError);
            }
            else if (exception is AuthorizationException)
            {
                ShowMessage(AppResources.Error, AppResources.AuthorizationFailedError);
            }
            else
            {
                ShowMessage(AppResources.Error, AppResources.CriticalErrorHasOccured);
            }
        }

        private void ShowMessage(string title, string text)
        {
            _messagingService.Alert(title, text);
        }
    }
}
