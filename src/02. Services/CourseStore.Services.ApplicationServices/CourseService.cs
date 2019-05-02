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
