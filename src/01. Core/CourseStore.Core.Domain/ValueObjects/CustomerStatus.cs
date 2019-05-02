using System;
using System.Collections.Generic;
using System.Text;

namespace CourseStore.Core.Domain.ValueObjects
{
    public class CustomerStatus : BaseValueObject<CustomerStatus>
    {
        public static CustomerStatus Regular => new CustomerStatus(CustomerStatusType.Regular, ExpirationDate.Infinite);

        public CustomerStatusType Type { get; private set; }
        public DateTime? _expirationDate;
        public ExpirationDate ExpirationDate
        {
            get
            {
                return (ExpirationDate)_expirationDate;
            }

            private set
            {
                _expirationDate = ExpirationDate;
            }
        }
        protected CustomerStatus()
        {

        }
        private CustomerStatus(CustomerStatusType type, ExpirationDate expirationDate)
        {
            Type = type;
            ExpirationDate = expirationDate;

        }
        protected override int GetHashCodeCore()
        {
            return Type.GetHashCode() ^ ExpirationDate.GetHashCode();
        }

        protected override bool IsEqual(CustomerStatus other)
        {
            return Type == other.Type && ExpirationDate == other.ExpirationDate;
        }
        public bool IsAdvanced => Type == CustomerStatusType.Advanced && !ExpirationDate.IsExpired;

        public static CustomerStatus Promote()
        {
            return new CustomerStatus(CustomerStatusType.Advanced, (ExpirationDate)DateTime.UtcNow.AddYears(1));
        }
    }
    public enum CustomerStatusType
    {
        Regular = 1,
        Advanced = 2
    }

}
