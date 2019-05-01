using System;
using System.Collections.Generic;
using System.Linq;
using CourseStore.Core.Domain.Contracts;
using CourseStore.Core.Domain.Entities;
using CourseStore.Infrastructures.DataAccess.Repositories;
using CourseStore.Services.ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseStore.EndPoints.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerService _customerService;

        public CustomersController(ICourseRepository courseRepository, ICustomerRepository customerRepository, CustomerService customerService)
        {
            _customerRepository = customerRepository;
            _courseRepository = courseRepository;
            _customerService = customerService;
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

            return Json(customer);
        }

        [HttpGet]
        public JsonResult GetList()
        {
            IReadOnlyList<Customer> customers = _customerRepository.GetList();
            return Json(customers);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_customerRepository.GetByEmail(item.Email) != null)
                {
                    return BadRequest("ایمیل ورودی در حال حاضر ثبت شده است: " + item.Email);
                }

                item.Id = 0;
                item.Status = CustomerStatus.Regular;
                _customerRepository.Add(item);
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
        public IActionResult Update(long id, [FromBody] Customer item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Customer customer = _customerRepository.GetById(id);
                if (customer == null)
                {
                    return BadRequest("شناسه مشتری قابل قبول نیست: " + id);
                }

                customer.FirstName = item.FirstName;
                customer.LastName = item.LastName;

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

                if (customer.PurchasedCourses.Any(x => x.CourseId== course.Id && (x.ExpirationDate == null || x.ExpirationDate.Value >= DateTime.UtcNow)))
                {
                    return BadRequest("دوره منتخب در حال حاضر ثبت شده است: " + course.Name);
                }

                _customerService.PurchaseCourse(customer, course);

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

                if (customer.Status == CustomerStatus.Advanced && (customer.StatusExpirationDate == null || customer.StatusExpirationDate.Value < DateTime.UtcNow))
                {
                    return BadRequest("در حال حاضر کاربر در وضعیت پیشرفته وجود دارد.");
                }

                bool success = _customerService.PromoteCustomer(customer);
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
