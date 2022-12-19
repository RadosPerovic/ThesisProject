using Microsoft.EntityFrameworkCore;
using ThesisProject.Application.UseCases.Orders.Queries.GetOrderById;
using ThesisProject.Infrastructure.Persistence.Models;

namespace ThesisProject.Infrastructure.Persistence.Queries.Orders;
public class GetOrderByIdQuery : IGetOrderByIdQuery
{
    private readonly DatabaseContext _dbContext;

    public GetOrderByIdQuery(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderDto?> Execute(Guid orderId)
    {
        var dbModel = await _dbContext.Orders
            .Include(e => e.OrderItems)
                .ThenInclude(e => e.Product)
            .Where(e => e.Id == orderId)
            .SingleAsync();

        if (dbModel == null)
        {
            return null;
        }

        var order = GetOrderDto(dbModel);

        return order;
    }

    private OrderDto GetOrderDto(OrderModel orderModel)
    {
        return new OrderDto
        {
            Id = orderModel.Id,
            Name = orderModel.Name,
            OrderStatus = orderModel.OrderStatusType,
            Price = orderModel.CalculatedPrice,
            OrderItems = orderModel.OrderItems.Select(GetOrderItemDto).ToList(),
        };
    }

    private OrderItemDto GetOrderItemDto(OrderItemModel orderItemModel)
    {
        return new OrderItemDto
        {
            Product = GetProductDto(orderItemModel.Product),
            Quantity = orderItemModel.Quantity,
            CalculatedPrice = orderItemModel.Price
        };
    }

    private ProductDto GetProductDto(ProductModel productModel)
    {
        return new ProductDto
        {
            Id = productModel.Id,
            Name = productModel.Name,
            Price = productModel.Price,
        };
    }
}
