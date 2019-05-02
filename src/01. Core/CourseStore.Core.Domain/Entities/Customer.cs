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

        public void AddCourse(Course course)
        {
            ExpirationDate expirationDate = course.GetExpirationDate();
            Rial price = course.CalculatePrice(Status);
            var purchasedCourse = new PurchasedCourse(course, this, price, expirationDate);
            _purchasedCourses.Add(purchasedCourse);
            MoneySpent += price;
        }

        public void SetFullName(FullName value)
        {
            FullName = value;
        }
        public bool Promote()
        {
            // حداقل باید در ماه گذشته 2 دوره فعال داشته باشد
            if (PurchasedCourses.Count(x => x.ExpirationDate == ExpirationDate.Infinite || x.ExpirationDate.Date >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // حد اقل 100 هزار تومان در طی سال گذشته هزینه کرده باشد
            if (PurchasedCourses.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return false;

            Status = CustomerStatus.Promote();

            return true;
        }
    }
}
