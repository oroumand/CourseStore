using CourseStore.Core.Domain.Utilities.CourseStore.Core.Domain.Utilities;
using System;
using System.Text.RegularExpressions;

namespace CourseStore.Core.Domain.ValueObjects
{
    public class Email : BaseValueObject<Email>
    {
        public string Value { get; private set; }
        private Email(string value)
        {
            Value = value;
        }
        public Email() { }
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
