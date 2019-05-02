using CourseStore.Core.Domain.Entities;
using Newtonsoft.Json;

namespace CourseStore.Core.Domain.Dtos
{
    public class CreateCustomerDto
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
    public class CourseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public LicensingModel LicensingModel { get; set; }
    }
}
