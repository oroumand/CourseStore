using Newtonsoft.Json;
using System;

namespace CourseStore.Core.Domain.Dtos
{
    public class PurchasedCourseDto
    {
        public long Id { get; set; }
        [JsonIgnore]
        public virtual long CourseId { get; set; }

        public virtual CourseDto Course { get; set; }

        [JsonIgnore]
        public virtual long CustomerId { get; set; }

        public virtual decimal Price { get; set; }

        public virtual DateTime PurchaseDate { get; set; }

        public virtual DateTime? ExpirationDate { get; set; }
    }
}
