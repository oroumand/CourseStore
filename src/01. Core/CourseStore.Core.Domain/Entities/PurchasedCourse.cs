using CourseStore.Core.Domain.ValueObjects;
using Newtonsoft.Json;
using System;

namespace CourseStore.Core.Domain.Entities
{
    public class PurchasedCourse : BaseEntity
    {
        public long CourseId { get; set; }

        public Course Course { get; set; }

        public long CustomerId { get; set; }

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public ExpirationDate ExpirationDate { get; set; }
    }
}
