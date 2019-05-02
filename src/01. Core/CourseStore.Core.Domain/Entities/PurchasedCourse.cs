using CourseStore.Core.Domain.ValueObjects;
using Newtonsoft.Json;
using System;

namespace CourseStore.Core.Domain.Entities
{
    public class PurchasedCourse : BaseEntity
    {
        public Course Course { get; private  set; }

        public Customer Customer { get; private  set; }

        public decimal Price { get; private  set; }

        public DateTime PurchaseDate { get; private  set; }

        public ExpirationDate ExpirationDate { get; private set; }
        internal PurchasedCourse(Course course,Customer customer,Rial price, ExpirationDate expirationDate)
        {
            if (price == null || price.IsZero)
                throw new ArgumentException(nameof(price));
            if(expirationDate== null || expirationDate.IsExpired)
            {
                throw new ArgumentException(nameof(expirationDate));

            }
            Course = course ?? throw new ArgumentNullException(nameof(course));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Price = price;
            ExpirationDate = expirationDate;
        }
        private PurchasedCourse()
        {

        }
    }
}
