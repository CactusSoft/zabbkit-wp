using System;
using System.Windows;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Localization;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly IApplicationConfiguration _applicationConfiguration;

        public MessagingService(IApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        public void Alert(string title, string message)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            MessageBox.Show(message, title, MessageBoxButton.OK);
        }

        public bool Message(string title, string message)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return MessageBox.Show(message, title, MessageBoxButton.OKCancel) == MessageBoxResult.OK;
        }

        public void RemindToRateApp()
        {
            var rateReminder = new RadRateApplicationReminder
            {
                RecurrencePerUsageCount = _applicationConfiguration.ReminderRecurrencePerUsageCount,
                MessageBoxInfo =
                {
                    Content = AppResources.PleaseRateApplicationMessage,
                    Title = AppResources.PleaseRateApplicationMessageTitle,
                    SkipFurtherRemindersMessage = AppResources.PleaseRateApplicationDoNotShowAgainText
                },
                AllowUsersToSkipFurtherReminders = true
            };

            rateReminder.Notify();
        }
    }
}
