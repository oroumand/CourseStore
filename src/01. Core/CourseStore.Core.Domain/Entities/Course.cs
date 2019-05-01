using Newtonsoft.Json;

namespace CourseStore.Core.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public LicensingModel LicensingModel { get; set; }
    }
}
