using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThesisProject.Application.Services;
using ThesisProject.Application.UseCases.Orders.Queries.GetOrderById;
using ThesisProject.Application.UseCases.Products.Queries.GetProductById;
using ThesisProject.Domain.Repositories;
using ThesisProject.Infrastructure.Persistence;
using ThesisProject.Infrastructure.Persistence.Queries.Orders;
using ThesisProject.Infrastructure.Persistence.Queries.Products;
using ThesisProject.Infrastructure.Persistence.Repositories;
using ThesisProject.Infrastructure.Services;

namespace ThesisProject.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddEntityFramework(services, configuration);
        AddRepositories(services);
        AddQueries(services);
        AddServices(services);
    }

    private static void AddEntityFramework(IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration["SqlServerConfiguration:ConnectionString"];

        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }

    private static void AddQueries(IServiceCollection services)
    {
        services.AddScoped<IGetProductByIdQuery, GetProductByIdQuery>();
        services.AddScoped<IGetOrderByIdQuery, GetOrderByIdQuery>();
    }
    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IIdentityGenerator, IdentityGenerator>();
    }
}
