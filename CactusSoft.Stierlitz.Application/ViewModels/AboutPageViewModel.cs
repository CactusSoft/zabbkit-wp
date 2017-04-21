using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Tasks;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
	public class AboutPageViewModel : Screen
	{
        private readonly IApplicationConfiguration _applicationConfiguration;
	    private readonly IAnalyticsService _analyticsService;

	    public AboutPageViewModel(IApplicationConfiguration applicationConfiguration, IAnalyticsService analyticsService)
        {
            _applicationConfiguration = applicationConfiguration;
            _analyticsService = analyticsService;
        }

	    public IApplicationConfiguration ApplicationConfiguration
        {
            get { return _applicationConfiguration; }
        }

        public void ContactUs()
        {
            _analyticsService.ContactUs();
            var emailComposer = new EmailComposeTask
            {
                Subject = AppResources.SupportMailSubject,
                To = _applicationConfiguration.SupportMail
            };

            emailComposer.Show();
        }
	}
}