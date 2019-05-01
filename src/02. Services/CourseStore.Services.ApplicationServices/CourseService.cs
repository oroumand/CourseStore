using CourseStore.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseStore.Services.ApplicationServices
{
    public class CourseService
    {
        public DateTime? GetExpirationDate(LicensingModel licensingModel)
        {
            DateTime? result;

            switch (licensingModel)
            {
                case LicensingModel.TwoDays:
                    result = DateTime.UtcNow.AddDays(2);
                    break;

                case LicensingModel.LifeLong:
                    result = null;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
