using CourseStore.Core.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CourseStore.Core.Domain.ValueObjects
{
    public class FullName : BaseValueObject<FullName>
    {
        public string FirstName { get;private set; }


        public string LastName { get; private set; }
        private FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        private FullName() { }

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
            return FirstName.Equals(other.FirstName, StringComparison.InvariantCultureIgnoreCase) && LastName.Equals(other.LastName, StringComparison.InvariantCultureIgnoreCase);
        }
    }


    public class Email:BaseValueObject<Email>
    {
        public string Value { get; private set; }
        private Email(string value)
        {
            Value = value;
        }
        private Email() { }
        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Fail<Email>("برای ایمیل مقدار اجباری است");
            if (!Regex.IsMatch(email, "^(.+)@(.+)$"))
                return Result.Fail<Email>("ایمیل وارد شده قابل قبول نمی‌‌باشد.");

            return Result.Ok(new Email(email));

        }
        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        protected override bool IsEqual(Email other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
