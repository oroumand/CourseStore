using CourseStore.Core.Domain.Utilities.CourseStore.Core.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseStore.Core.Domain.ValueObjects
{
    public class FullName : BaseValueObject<FullName>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        private FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public FullName() { }

        public static Result<FullName> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Fail<FullName>("برای نام مقدار لازم است");
            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Fail<FullName>("برای نام خانوادگی مقدار لازم است");
            if (firstName.Length > 100)
            {
                return Result.Fail<FullName>("طول نام حداکثر 100 کاراکتر می‌باشد.");

            }
            if (lastName.Length > 100)
            {
                return Result.Fail<FullName>("طول نام خانوادگی حداکثر 100 کاراکتر می‌باشد.");
            }
            return Result.Ok(new FullName(firstName, lastName));
        }

        protected override int GetHashCodeCore()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode();
        }

        protected override bool IsEqual(FullName other)
        {
            return FirstName.Equals(other.FirstName,StringComparison.InvariantCultureIgnoreCase) && LastName.Equals(other.LastName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
