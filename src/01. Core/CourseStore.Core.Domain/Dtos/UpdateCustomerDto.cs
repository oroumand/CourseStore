using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseStore.Core.Domain.Dtos
{
    public class UpdateCustomerDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "حداکثر طول نام 100 کاراکتر است.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "حداکثر طول نام خانوادگی 100 کاراکتر است.")]
        public string LastName { get; set; }
    }
}
