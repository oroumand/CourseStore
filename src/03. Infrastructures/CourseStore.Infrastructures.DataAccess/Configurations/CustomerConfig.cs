using CourseStore.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CourseStore.Infrastructures.DataAccess.Configurations
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(100).IsRequired();
            builder.HasData(
                new Customer
                {
                    FirstName = "محمد",
                    LastName = "لطفی",
                    Email = "mlotfi@gmail.com",
                    MoneySpent = 200_000,
                    Status = CustomerStatus.Advanced,
                    StatusExpirationDate = DateTime.Now.AddDays(15),
                    Id = 1
                },
                new Customer
                {
                    FirstName = "آرش",
                    LastName = "اژدری",
                    Email = "a.Azhdari@gmail.com",
                    MoneySpent = 20_000,
                    Status = CustomerStatus.Regular,
                    Id = 2
                }
                );
        }
    }
}
