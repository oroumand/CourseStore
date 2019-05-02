using System;
using System.Collections.Generic;
using System.Linq;
using CourseStore.Core.Domain.Contracts;
using CourseStore.Core.Domain.Dtos;
using CourseStore.Core.Domain.Entities;
using CourseStore.Core.Domain.Utilities.CourseStore.Core.Domain.Utilities;
using CourseStore.Core.Domain.ValueObjects;

using Microsoft.AspNetCore.Mvc;

namespace CourseStore.EndPoints.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICourseRepository courseRepository, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _courseRepository = courseRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            Customer customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            var dto = new CustomerDto
            {
                Id = customer.Id,

                FirstName = customer.FullName.FirstName,
                LastName = customer.FullName.LastName,
                Email = customer.Email.Value,
                MoneySpent = customer.MoneySpent.Value,
                Status = customer.Status.Type,
                StatusExpirationDate = customer.Status.ExpirationDate,
                PurchasedCourses = customer.PurchasedCourses.Select(x => new PurchasedCourseDto
                {
                    Price = x.Price,
                    ExpirationDate = x.ExpirationDate,
                    PurchaseDate = x.PurchaseDate,
                    Course = new CourseDto
                    {
                        Id = x.Course.Id,
                        Name = x.Course.Name
                    }
                }).ToList()
            };
            return Json(customer);
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var customers = _customerRepository.GetList();
            var dtos = customers.Select(x => new CustomerInListDto
            {
                Id = x.Id,
                FirstName = x.FullName.FirstName,
                LastName = x.FullName.LastName,
                Email = x.Email.Value,
                MoneySpent = x.MoneySpent.Value,
                Status = x.Status.Type.ToString(),
                StatusExpirationDate = x.Status.ExpirationDate
            }).ToList();
            return Json(customers);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerDto item)
        {
            try
            {
                var fullName = FullName.Create(item.FirstName, item.LastName);
                var email = Email.Create(item.Email);
                var result = Result.Combine(fullName, email);
                if (result.IsFailure)
                    return BadRequest(result.Error);

                if (_customerRepository.GetByEmail(email.Value) != null)
                {
                    return BadRequest("ایمیل ورودی در حال حاضر ثبت شده است: " + item.Email);
                }

                var customer = new Customer(fullName.Value, email.Value);
                _customerRepository.Add(customer);
                _customerRepository.Save();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(long id, [FromBody] UpdateCustomerDto item)
        {
            try
            {
                var fullName = FullName.Create(item.FirstName, item.LastName);
                if (fullName.IsFailure)
                    return BadRequest(fullName.Error);

                Customer customer = _customerRepository.GetById(id);
                if (customer == null)
                {
                    return BadRequest("شناسه مشتری قابل قبول نیست: " + id);
                }

                customer.SetFullName( fullName.Value);
                _customerRepository.Save();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        [Route("{id}/course")]
        public IActionResult PurchaseCourse(long id, [FromQuery] long courseId)
        {
            try
            {
                Course course = _courseRepository.GetById(courseId);
                if (course == null)
                {
                    return BadRequest("شناسه دوره قابل قبول نیست: " + courseId);
                }

                Customer customer = _customerRepository.GetById(id);
                if (customer == null)
                {
                    return BadRequest("شناسه مشتری قابل قبول نیست: " + id);
                }

                if (customer.PurchasedCourses.Any(x => x.Course?.Id == course.Id && !x.ExpirationDate.IsExpired))
                {
                    return BadRequest("دوره منتخب در حال حاضر ثبت شده است: " + course.Name);
                }
                customer.AddCourse(course);
                _customerRepository.Save();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        [Route("{id}/promotion")]
        public IActionResult PromoteCustomer(long id)
        {
            try
            {
                Customer customer = _customerRepository.GetById(id);
                if (customer == null)
                {
                    return BadRequest("شناسه مشتری قابل قبول نیست: " + id);
                }

                if (customer.Status.IsAdvanced)
                {
                    return BadRequest("در حال حاضر کاربر در وضعیت پیشرفته وجود دارد.");
                }
                bool success = customer.Promote();
                if (!success)
                {
                    return BadRequest("امکان ارتقا وضعیت کاربر وجود ندارد.");
                }

                _customerRepository.Save();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
