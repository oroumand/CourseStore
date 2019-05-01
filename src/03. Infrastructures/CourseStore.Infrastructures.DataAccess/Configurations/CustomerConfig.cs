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
            builder.OwnsOne(c => c.FullName,d=> {
                d.Property(e=>e.FirstName).HasMaxLength(100).IsRequired().HasColumnName("FirstName");
                d.Property(e=>e.LastName).HasMaxLength(100).IsRequired().HasColumnName("LastName");
            });
            builder.Property(c => c.Email).HasConversion(c => c.Value, d =>  Core.Domain.ValueObjects.Email.Create(d).Value);
            //builder.HasData(
            //    new Customer
            //    {
            //        FullName = new Core.Domain.ValueObjects.FullName("محمد", "لطفی"),
            //        Email = new Core.Domain.ValueObjects.Email("mlotfi@gmail.com"),
            //        MoneySpent = 200_000,
            //        Status = CustomerStatus.Advanced,
            //        StatusExpirationDate = DateTime.Now.AddDays(15),
            //        Id = 1
            //    },
            //    new Customer
            //    {
            //        FullName = new Core.Domain.ValueObjects.FullName("آرش", "اژدری"),
            //        Email = new Core.Domain.ValueObjects.Email("a.Azhdari@gmail.com"),
            //        MoneySpent = 20_000,
            //        Status = CustomerStatus.Regular,
            //        Id = 2
            //    }
            //    );
        }
    }
}
