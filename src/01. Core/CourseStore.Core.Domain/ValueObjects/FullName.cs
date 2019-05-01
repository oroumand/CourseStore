using System;
using System.Collections.Generic;
using System.Text;

namespace CourseStore.Core.Domain.ValueObjects
{
    public class FullName : BaseValueObject<FullName>
    {
        public string FirstName { get; }
        public string LastName { get; }
        public FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
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
