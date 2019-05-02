using Newtonsoft.Json;
using System;

namespace CourseStore.Core.Domain.Dtos
{
    public class PurchasedCourseDto
    {
        public long Id { get; set; }
        [JsonIgnore]
        public long CourseId { get; set; }

        public CourseDto Course { get; set; }

        [JsonIgnore]
        public long CustomerId { get; set; }

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
