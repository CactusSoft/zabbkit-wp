using System;
using System.Diagnostics;
using System.Windows;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Services.Web;
using Microsoft.Phone.Notification;

namespace CactusSoft.Stierlitz.Application.Services
{
    public class PushChannelService : IPushChannelService
    {
        private readonly IErrorReporter _errorHandler;
        private readonly IDeviceManagementService _deviceManagementService;
        private HttpNotificationChannel _httpChannel;

        public event EventHandler<EventArgs<string>> ManagementIdChanged;
        public event EventHandler<EventArgs> Error;

        public PushChannelService(IErrorReporter errorHandler, IDeviceManagementService deviceManagementService)
        {
            _errorHandler = errorHandler;
            _deviceManagementService = deviceManagementService;
        }

        public Uri Uri { get { return _httpChannel != null ? _httpChannel.ChannelUri : null; } }

        public string ManagementId { get; private set; }

        public void Connect()
        {
            try
            {
                const string channelName = "ZabbkitNotifications";
                _httpChannel = HttpNotificationChannel.Find(channelName);
                if (null == _httpChannel)
                {
                    _httpChannel = new HttpNotificationChannel(channelName);
                    SubscribeToChannelEvents();
                    _httpChannel.Open();
                    SubscribeToNotifications();
                }
                else
                {
                    SubscribeToChannelEvents();
                    SubscribeToManagementServiceAsync();
                }
            }
            catch (Exception ex)
            {
                //_errorHandler.WriteReport(string.Format("Channel error: {0}",ex.Message));
                OnError();
            }
        }

        private void OnManagementIdChanged(string newId)
        {
            if (ManagementIdChanged != null)
                ManagementIdChanged(this, new EventArgs<string>(newId));
        }

        private void OnError()
        {
            if (Error != null)
                Error(this, null);
        }
        private void SubscribeToManagementServiceAsync()
        {
            var task = _deviceManagementService.RegisterDevice(Uri.AbsoluteUri).ContinueWith(result =>
            {
                ManagementId = result.Result;
                OnManagementIdChanged(ManagementId);
            });
            if (task.IsFaulted || task.IsCanceled)
                OnError();
        }

        private void SubscribeToNotifications()
        {
            try
            {
                if (!_httpChannel.IsShellToastBound)
                    _httpChannel.BindToShellToast();
            }
            catch (InvalidOperationException iopEx)
            {
                //_errorHandler.WriteReport(string.Format("Channel error: {0}", iopEx));
            }

            try
            {
                if (!_httpChannel.IsShellTileBound)
                    _httpChannel.BindToShellTile();
            }
            catch (InvalidOperationException iopEx)
            {
                //_errorHandler.WriteReport(string.Format("Channel error: {0}", iopEx));
            }
        }

        private void SubscribeToChannelEvents()
        {
            _httpChannel.ChannelUriUpdated += OnHttpChannelChannelUriUpdated;
            _httpChannel.ErrorOccurred += OnHttpChannelErrorOccurred;
        }

        private static void OnHttpChannelErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => UpdateStatus(e.Message));
        }

        private void OnHttpChannelChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            SubscribeToNotifications();
            OnManagementIdChanged(null);
            SubscribeToManagementServiceAsync();
            Deployment.Current.Dispatcher.BeginInvoke(() => UpdateStatus(string.Format("Channel created successfully: {0}", e.ChannelUri)));
        }

        private static void UpdateStatus(string message)
        {
            Debug.WriteLine(message);
        }

    }
}
