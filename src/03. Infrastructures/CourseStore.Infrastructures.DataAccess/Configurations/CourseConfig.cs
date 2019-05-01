using CourseStore.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseStore.Infrastructures.DataAccess.Configurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
            builder.HasData(
            new Course
            {
                LicensingModel = LicensingModel.TwoDays,
                Name = "کارگاه DDD",
                Id = 1
            },
            new Course
            {
                LicensingModel = LicensingModel.LifeLong,
                Name = "دوره آموزشی NoSQLهای پرکاربرد",
                Id = 2
            },
            new Course
            {
                LicensingModel = LicensingModel.LifeLong,
                Name = "دوره آموزش ASP.NET Core 3.0 پیشرفته ",
                Id = 3
            }
            );
        }
    }
}
