using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThesisProject.Infrastructure.Persistence.Models;

namespace ThesisProject.Infrastructure.Persistence.Configurations;
public class OrderModelConfiguration : IEntityTypeConfiguration<OrderModel>
{
    public void Configure(EntityTypeBuilder<OrderModel> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.OrderStatusType)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasMaxLength(50);

        builder.HasOne(e => e.OrderStatus)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.OrderStatusType)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
