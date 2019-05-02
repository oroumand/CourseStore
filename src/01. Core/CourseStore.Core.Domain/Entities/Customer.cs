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

        public Email Email { get; set; }

        public CustomerStatus Status { get; set; }

        public ExpirationDate StatusExpirationDate { get; set; }

        public Rial MoneySpent { get; set; }

        public IList<PurchasedCourse> PurchasedCourses { get; set; }

        public void SetFullName(FullName value)
        {
            FullName = value;
        }
    }
}
