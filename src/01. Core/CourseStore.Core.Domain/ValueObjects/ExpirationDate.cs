using CourseStore.Core.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseStore.Core.Domain.ValueObjects
{
    public class ExpirationDate : BaseValueObject<ExpirationDate>
    {
        public static readonly ExpirationDate Infinite = new ExpirationDate(null);

        public DateTime? Date { get; private set; }

        public bool IsExpired => this != Infinite && Date < DateTime.UtcNow;

        private ExpirationDate(DateTime? date)
        {
            Date = date;
        }

        public static Result<ExpirationDate> Create(DateTime date)
        {
            return Result.Ok(new ExpirationDate(date));
        }

        protected override bool IsEqual(ExpirationDate other)
        {
            return Date == other.Date;
        }

        protected override int GetHashCodeCore()
        {
            return Date.GetHashCode();
        }

        public static explicit operator ExpirationDate(DateTime? date)
        {
            if (date.HasValue)
                return Create(date.Value).Value;

            return Infinite;
        }

        public static implicit operator DateTime? (ExpirationDate date)
        {
            return date?.Date;
        }
    }
}
