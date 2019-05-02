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

        private decimal CalculatePrice(CustomerStatus status, ExpirationDate statusExpirationDate, LicensingModel licensingModel)
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

            if (status.IsAdvanced && (!statusExpirationDate.IsExpired))
            {
                price = price * 0.75m;
            }

            return price;
        }

        public void PurchaseCourse(Customer customer, Course course)
        {
            var expirationDate = _courseService.GetExpirationDate(course.LicensingModel);
            decimal price = CalculatePrice(customer.Status, customer.Status.ExpirationDate, course.LicensingModel);

            var purchasedMovie = new PurchasedCourse
            {
                CourseId = course.Id,
                CustomerId = customer.Id,
                ExpirationDate = expirationDate,
                Price = price
            };

            customer.PurchasedCourses.Add(purchasedMovie);
            customer.MoneySpent += Rial.of(price);
        }

    }
}
