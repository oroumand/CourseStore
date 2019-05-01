using Newtonsoft.Json;

namespace CourseStore.Core.Domain.Entities
{
    public class Course : BaseEntity
    {
        public virtual string Name { get; set; }

        [JsonIgnore]
        public virtual LicensingModel LicensingModel { get; set; }
    }
}
