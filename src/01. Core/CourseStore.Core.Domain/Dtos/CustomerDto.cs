using CourseStore.Core.Domain.Entities;
using CourseStore.Core.Domain.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseStore.Core.Domain.Dtos
{
    public class UpdateCustomerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
    public class CustomerDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CustomerStatusType Status { get; set; }

        public DateTime? StatusExpirationDate { get; set; }

        public decimal MoneySpent { get; set; }

        public IList<PurchasedCourseDto> PurchasedCourses { get; set; }
    }
}
