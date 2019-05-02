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


        public Rial CalculatePrice(CustomerStatus status)
        {
            Rial price;
            switch (LicensingModel)
            {
                case LicensingModel.TwoDays:
                    price = Rial.of(4);
                    break;

                case LicensingModel.LifeLong:
                    price = Rial.of(8);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (status.IsAdvanced)
            {
                price *= 0.75m;
            }

            return price;
        }
    }
}
