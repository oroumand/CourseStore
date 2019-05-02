using CourseStore.Core.Domain.Entities;
using CourseStore.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CourseStore.Infrastructures.DataAccess.Configurations
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.OwnsOne(c => c.FullName, d =>
            {
                d.Property(e => e.FirstName).HasMaxLength(100).IsRequired().HasColumnName("FirstName");
                d.Property(e => e.LastName).HasMaxLength(100).IsRequired().HasColumnName("LastName");
            });

            builder.Property(c => c.Email).HasConversion(c => c.Value, d => Email.Create(d).Value);
            builder.Property(c => c.MoneySpent).HasConversion(c => c.Value, d => Rial.of(d));
            builder.OwnsOne(c => c.Status, d =>
            {
                d.Property(e => e.Type).IsRequired().HasColumnName("Status");
                d.Property(e => e.ExpirationDate).HasConversion(c => c.Date, c => (ExpirationDate)c).HasColumnName("StatusExpirationDate");
            });
        }
    }
}
