using CourseStore.Core.Domain.ValueObjects;
using Newtonsoft.Json;
using System;

namespace CourseStore.Core.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public LicensingModel LicensingModel { get; set; }


        public ExpirationDate GetExpirationDate()
        {
            ExpirationDate result;

            switch (LicensingModel)
            {
                case LicensingModel.TwoDays:
                    result = (ExpirationDate)DateTime.UtcNow.AddDays(2);
                    break;

                case LicensingModel.LifeLong:
                    result = ExpirationDate.Infinite;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
