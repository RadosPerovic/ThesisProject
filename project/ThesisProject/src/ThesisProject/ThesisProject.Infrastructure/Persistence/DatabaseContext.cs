using Microsoft.EntityFrameworkCore;
using ThesisProject.Domain.Enums;
using ThesisProject.Infrastructure.Persistence.Models;

namespace ThesisProject.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    public DbSet<ProductModel> Products { get; set; }
    public DbSet<OrderStatusModel> OrderStatuses { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderItemModel> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        Seed(builder);
    }

    private void Seed(ModelBuilder builder)
    {
        SeedEnumModels<OrderStatusModel, OrderStatus>(builder, e => new OrderStatusModel { Id = e, Name = e.ToString() });
    }

    private void SeedEnumModels<TModel, TEnum>(ModelBuilder builder, Func<TEnum, TModel> converter)
        where TModel : class
    {
        var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        var models = values.Select(converter)
                           .ToList();

        SeedModels(builder, models);
    }

    private void SeedModels<TModel>(ModelBuilder builder, List<TModel> models)
        where TModel : class
    {
        builder.Entity<TModel>().HasData(models);
    }
}