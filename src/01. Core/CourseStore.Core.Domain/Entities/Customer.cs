using CourseStore.Core.Domain.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseStore.Core.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public FullName FullName{ get; set; }
        public  Email Email { get; set; }
        public CustomerStatus Status { get; set; }
        public DateTime? StatusExpirationDate { get; set; }
        public decimal MoneySpent { get; set; }
        public IList<PurchasedCourse> PurchasedCourses { get; set; }
    }
}
