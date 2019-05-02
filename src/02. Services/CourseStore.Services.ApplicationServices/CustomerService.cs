using CourseStore.Core.Domain.Entities;
using CourseStore.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseStore.Services.ApplicationServices
{
    public class CustomerService
    {
        private readonly CourseService _courseService;

        public CustomerService(CourseService courseService)
        {
            _courseService = courseService;
        }

        private Rial CalculatePrice(CustomerStatus status, DateTime? statusExpirationDate, LicensingModel licensingModel)
        {
            Rial price;
            switch (licensingModel)
            {
                case LicensingModel.TwoDays:
                    price = Rial.of(4);
                    break;

                case LicensingModel.LifeLong:
                    price = Rial.of(8);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (status == CustomerStatus.Advanced && (statusExpirationDate == null || statusExpirationDate.Value >= DateTime.UtcNow))
            {
                price = price * 0.75m;
            }

            return price;
        }

        public void PurchaseCourse(Customer customer, Course course)
        {
            ExpirationDate expirationDate = _courseService.GetExpirationDate(course.LicensingModel);
            Rial price = CalculatePrice(customer.Status, customer.StatusExpirationDate, course.LicensingModel);

            var purchasedMovie = new PurchasedCourse
            {
                CourseId = course.Id,
                CustomerId = customer.Id,
                ExpirationDate = expirationDate,
                Price = price
            };

            customer.PurchasedCourses.Add(purchasedMovie);
            customer.MoneySpent += price;
        }

        public bool PromoteCustomer(Customer customer)
        {
            // حداقل باید در ماه گذشته 2 دوره فعال داشته باشد
            if (customer.PurchasedCourses.Count(x => x.ExpirationDate == ExpirationDate.Infinite || x.ExpirationDate.Date >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // حد اقل 100 هزار تومان در طی سال گذشته هزینه کرده باشد
            if (customer.PurchasedCourses.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return false;

            customer.Status = CustomerStatus.Advanced;
            customer.StatusExpirationDate = (ExpirationDate)DateTime.UtcNow.AddYears(1);

            return true;
        }
    }
}
