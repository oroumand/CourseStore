using CourseStore.Core.Domain.Entities;
using CourseStore.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CourseStore.Infrastructures.DataAccess.Configurations
{
    public class PurchasedCourseConfig : IEntityTypeConfiguration<PurchasedCourse>
    {
        public void Configure(EntityTypeBuilder<PurchasedCourse> builder)
        {
            builder.Property(c => c.ExpirationDate).HasConversion(c => (DateTime?)c, d => (ExpirationDate)d);
            builder.HasData(
                new PurchasedCourse
                {
                    CourseId = 1,
                    CustomerId = 1,
                    Price = 20_000,
                    Id = 1,
                    ExpirationDate = (ExpirationDate)DateTime.Now.AddDays(1),
                    PurchaseDate = DateTime.Now.AddDays(-1),

                },
                new PurchasedCourse
                {
                    CourseId = 2,
                    CustomerId = 1,
                    Price = 80_000,
                    Id = 2,

                    PurchaseDate = DateTime.Now.AddDays(-10),

                },
                new PurchasedCourse
                {
                    CourseId = 3,
                    CustomerId = 1,
                    Price = 100_000,
                    Id = 3,
                    PurchaseDate = DateTime.Now.AddDays(-1),

                },
                new PurchasedCourse
                {
                    CourseId = 1,
                    CustomerId = 1,
                    Price = 20_000,
                    Id = 4,
                    ExpirationDate = (ExpirationDate)DateTime.Now.AddDays(1),
                    PurchaseDate = DateTime.Now.AddDays(-1),

                }

            );
        }
    }
}
