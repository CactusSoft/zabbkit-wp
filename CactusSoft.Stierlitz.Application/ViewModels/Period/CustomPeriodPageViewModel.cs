using System;
using CactusSoft.Stierlitz.Application.Messages;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.Period
{
    public class CustomPeriodPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private DateTime _date;
        private DateTime _time;
        private TimeSpan _period;
        private bool _dateTimeChanged;

        public CustomPeriodPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            Initialize();
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                NotifyOfPropertyChange(() => Date);
            }
        }

        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                NotifyOfPropertyChange(() => Time);
            }
        }

        public TimeSpan Period
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
                NotifyOfPropertyChange(() => Period);
            }
        }

        public void OnDateTimeChanged()
        {
            _dateTimeChanged = true;
        }

        public void Save()
        {
            var dateTime = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hour, Time.Minute, 0) - Period;
            DateTime? stime = _dateTimeChanged ? dateTime : (DateTime?) null;
            _eventAggregator.Publish(new PeriodChangedMessage{Period = Period, StartTime = stime});
            _navigationService.RemoveBackEntry();
            _navigationService.GoBack();
        }

        public void Cancel()
        {
            _navigationService.GoBack();
        }

        private void Initialize()
        {
            Date = Time = DateTime.Now;
            Period = TimeSpan.FromHours(1);
        }
    }
}
