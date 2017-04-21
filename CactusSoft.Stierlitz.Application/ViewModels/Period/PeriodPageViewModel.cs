using System;
using System.Collections.Generic;
using System.Linq;
using CactusSoft.Stierlitz.Application.Messages;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.Period
{
    public class PeriodPageViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private static readonly PeriodViewModel CustomPeriod = new PeriodViewModel(TimeSpan.MinValue, AppResources.Custom);
        private readonly TimeSpan[] _intervals = new[]
                                                    {
                                                        TimeSpan.FromHours(1),
                                                        TimeSpan.FromHours(2),
                                                        TimeSpan.FromHours(3),
                                                        TimeSpan.FromHours(6),
                                                        TimeSpan.FromHours(12),
                                                        TimeSpan.FromDays(1),
                                                        TimeSpan.FromDays(7),
                                                        TimeSpan.FromDays(14),
                                                        TimeSpan.FromDays(30),
                                                    };


        public PeriodPageViewModel(IEventAggregator eventAggregator, INavigationService navigationService)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            Initialize();
        }

        public IList<PeriodViewModel> Items
        {
            get;
            set;
        }

        public void SelectInterval(PeriodViewModel period)
        {
            if (!period.Equals(CustomPeriod))
            {
                _eventAggregator.Publish(new PeriodChangedMessage
                                             {
                                                 Period = period.Period
                                             });
                _navigationService.GoBack();
            }
            else
            {
                NavigateToCustomPeriod();
            }
        }

        private void NavigateToCustomPeriod()
        {
            _navigationService.UriFor<CustomPeriodPageViewModel>().Navigate();
        }

        private void Initialize()
        {
            Items = new List<PeriodViewModel>(_intervals.Select(i => new PeriodViewModel(i))) { CustomPeriod };
        }


    }
}
