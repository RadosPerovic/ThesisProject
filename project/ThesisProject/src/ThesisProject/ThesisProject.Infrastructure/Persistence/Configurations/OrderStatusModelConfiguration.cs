using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThesisProject.Infrastructure.Persistence.Models;

namespace ThesisProject.Infrastructure.Persistence.Configurations;
public class OrderStatusModelConfiguration : IEntityTypeConfiguration<OrderStatusModel>
{
    public void Configure(EntityTypeBuilder<OrderStatusModel> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedNever();
    }
}
