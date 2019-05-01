using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseStore.Core.Domain.Dtos
{
    public class UpdateCustomerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
