using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
    public class CustomerETC : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasAlternateKey(x => x.Name);
            builder.Property(x => x.Name)
                .HasMaxLength(Customer.NameMaxLength);
        }
    }
}