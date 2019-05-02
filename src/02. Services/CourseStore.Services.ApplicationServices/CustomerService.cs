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

        

        public void PurchaseCourse(Customer customer, Course course)
        {
            ExpirationDate expirationDate = course.GetExpirationDate();
            Rial price = course.CalculatePrice(customer.Status);
            customer.AddCourse(course,expirationDate,price);
            
        }


    }
}
