using CourseStore.Core.Domain.Entities;
using Newtonsoft.Json;

namespace CourseStore.Core.Domain.Dtos
{
    public class CourseDto : BaseEntity
    {
        public virtual string Name { get; set; }

        [JsonIgnore]
        public virtual LicensingModel LicensingModel { get; set; }
    }
}
