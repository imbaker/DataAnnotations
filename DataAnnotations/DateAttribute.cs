namespace DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DateAttribute : ValidationAttribute
    {
        public enum AllowedDates   
        {
            IsBeforeToday = 0,
            IsTodayAndBeforeToday = 1,
            IsTodayOnly = 2,
            IsTodayAndAfterToday = 3,
            IsAfterToday = 4,
        }
        
        private readonly AllowedDates _allowedDates;

        public DateAttribute(AllowedDates allowedDates)
        {
            this._allowedDates = allowedDates;
        }

        public override bool IsValid(object value)
        {
            var date = value as DateTime?;

            if (this._allowedDates == AllowedDates.IsBeforeToday)
            {
                return date < DateTime.Today;
            }

            if (this._allowedDates == AllowedDates.IsTodayAndBeforeToday)
            {
                return date <= DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
            }

            if (this._allowedDates == AllowedDates.IsTodayOnly)
            {
                return date >= DateTime.Today &&
                       date <= DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
            }

            if (this._allowedDates == AllowedDates.IsTodayAndAfterToday)
            {
                return date >= DateTime.Today;
            }

            if (this._allowedDates == AllowedDates.IsAfterToday)
            {
                return date >= DateTime.Today.AddDays(1);
            }

            return false;
        }
    }
}