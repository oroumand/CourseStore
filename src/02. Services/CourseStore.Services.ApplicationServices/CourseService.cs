using CourseStore.Core.Domain.Entities;
using CourseStore.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseStore.Services.ApplicationServices
{
    public class CourseService
    {
        public ExpirationDate GetExpirationDate(LicensingModel licensingModel)
        {
            ExpirationDate result;

            switch (licensingModel)
            {
                case LicensingModel.TwoDays:
                    result = ExpirationDate.Create( DateTime.UtcNow.AddDays(2)).Value;
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
