using CourseStore.Core.Domain.Entities;
using Newtonsoft.Json;

namespace CourseStore.Core.Domain.Dtos
{
    public class CourseDto : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public LicensingModel LicensingModel { get; set; }
    }
}
