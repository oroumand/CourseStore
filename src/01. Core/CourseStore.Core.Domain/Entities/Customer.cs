using CourseStore.Core.Domain.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CourseStore.Core.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public FullName FullName { get; protected set; }
        public Email Email { get; protected set; }
        public CustomerStatus Status { get; set; }
        public Rial MoneySpent { get; private set; }
        private HashSet<PurchasedCourse> _purchasedCourses;
        public IEnumerable<PurchasedCourse> PurchasedCourses => _purchasedCourses?.ToList();
        public Customer(FullName fullName, Email email) : base()
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            MoneySpent = Rial.of(0);
            Status = CustomerStatus.Regular;
        }
        private Customer()
        {
            _purchasedCourses = new HashSet<PurchasedCourse>();
        }

        public void AddCourse(Course course, ExpirationDate expirationDate, Rial price)
        {
            var purchasedCourse = new PurchasedCourse
            {
                CourseId = course.Id,
                CustomerId = Id,
                ExpirationDate = expirationDate,
                Price = price
            };

            _purchasedCourses.Add(purchasedCourse);
            MoneySpent += price;
        }

        public void SetFullName(FullName value)
        {
            FullName = value;
        }
    }
}
