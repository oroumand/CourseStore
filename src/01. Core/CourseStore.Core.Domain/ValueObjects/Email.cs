using System;

namespace CourseStore.Core.Domain.ValueObjects
{
    public class Email : BaseValueObject<Email>
    {
        public string Value { get; }
        public Email(string value)
        {
            Value = value;
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
