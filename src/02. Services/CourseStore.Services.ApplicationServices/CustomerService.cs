using CourseStore.Core.Domain.Entities;
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

        private decimal CalculatePrice(CustomerStatus status, DateTime? statusExpirationDate, LicensingModel licensingModel)
        {
            decimal price;
            switch (licensingModel)
            {
                case LicensingModel.TwoDays:
                    price = 4000;
                    break;

                case LicensingModel.LifeLong:
                    price = 8000;
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
            DateTime? expirationDate = _courseService.GetExpirationDate(course.LicensingModel);
            decimal price = CalculatePrice(customer.Status, customer.StatusExpirationDate, course.LicensingModel);

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
            if (customer.PurchasedCourses.Count(x => x.ExpirationDate == null || x.ExpirationDate.Value >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // حد اقل 100 هزار تومان در طی سال گذشته هزینه کرده باشد
            if (customer.PurchasedCourses.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return false;

            customer.Status = CustomerStatus.Advanced;
            customer.StatusExpirationDate = DateTime.UtcNow.AddYears(1);

            return true;
        }
    }
}
