using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseStore.Core.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual CustomerStatus Status { get; set; }
        public virtual DateTime? StatusExpirationDate { get; set; }
        public virtual decimal MoneySpent { get; set; }
        public virtual IList<PurchasedCourse> PurchasedCourses { get; set; }
    }
}
