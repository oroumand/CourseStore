using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CourseStore.Core.Domain.Dtos
{
    public class CreateCustomerDto
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
