using System;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Localization;

namespace CactusSoft.Stierlitz.Application.ViewModels.Period
{
    public class PeriodViewModel : IEquatable<PeriodViewModel>
    {
        private const string NAME_FORMAT = "{0} {1}";
        public PeriodViewModel(TimeSpan period)
        {
            Period = period;
            Name = GetName(period);
        }

        public PeriodViewModel(TimeSpan period, string name)
        {
            Period = period;
            Name = name;
        }

        public TimeSpan Period
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        private string GetName(TimeSpan period)
        {
            string name;
            if (period.TotalDays < 1)
            {
                name = string.Format(NAME_FORMAT, Period.Hours, PluralFormHelper.GetPluralForm(Period.Hours, AppResources.HoursFirstForm, AppResources.HoursSecondForm, AppResources.HoursThirdForm));
            }
            else if (period.Days < 30)
            {
                name = string.Format(NAME_FORMAT, Period.Days, PluralFormHelper.GetPluralForm(Period.Days, AppResources.DayFirstForm, AppResources.DaySecondForm, AppResources.DayThirdForm));
            }
            else
            {
                
                var months = (int) Math.Floor(Period.Days / 30f);
                name = string.Format(NAME_FORMAT, months, PluralFormHelper.GetPluralForm(months, AppResources.MonthFirstForm, AppResources.MonthSecondForm, AppResources.MonthThirdForm));
            }

            return name;
        }

        public override int GetHashCode()
        {
            return Period.GetHashCode();
        }

        public bool Equals(PeriodViewModel other)
        {
            return Period.Equals(other.Period);
        }

        public override bool Equals(object obj)
        {
            var other = obj as PeriodViewModel;
            if (other == null)
            {
                return false;
            }
            return Equals(other);
        }

    }
}
