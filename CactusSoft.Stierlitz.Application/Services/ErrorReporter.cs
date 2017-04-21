using System;
using System.Reflection;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Localization;
using Microsoft.Phone.Info;
using Microsoft.Phone.Tasks;

namespace CactusSoft.Stierlitz.Application.Services
{
    public class ErrorReporter : IErrorReporter
    {
        private const string MAIL_BODY_FORMAT = "Device: {0}\nApplication version: {1}\n\nStack trace:\n{2}";

        private const string LOG_NAME = "Crash_Report";
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly ILogService _logService;

        public ErrorReporter(IApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
            _logService = new LogService
                    {
                        LogFileName = LOG_NAME
                    };
        }

        public void SendReport()
        {
            if (!HasReports())
            {
                return;
            }
            var deviceInfo = string.Format("{0} {1} {2} {3} {4}", 
                DeviceStatus.DeviceManufacturer, 
                DeviceStatus.DeviceName,
                Environment.OSVersion.Platform, 
                Environment.OSVersion.Version, 
                DeviceStatus.DeviceFirmwareVersion);
            var appVersion = Assembly.GetExecutingAssembly().GetVersion();
            var stackTrace = _logService.GetLastReport();
            _logService.DeleteAll();

            var body = string.Format(MAIL_BODY_FORMAT, deviceInfo, appVersion, stackTrace);

            var emailComposer = new EmailComposeTask
                                    {
                                        Body = body, 
                                        Subject = AppResources.ErrorMailSubject, 
                                        To = _applicationConfiguration.SupportMail
                                    };

            emailComposer.Show();
        }

        public bool HasReports()
        {
            return _logService.LogsExists;
        }

        public void WriteReport(string report)
        {
            _logService.Add(report);
            _logService.Save();
        }

        public void ClearReports()
        {
            _logService.DeleteAll();
        }
    }
}
