using Microsoft.EntityFrameworkCore;
using ThesisProject.Domain.Entities;
using ThesisProject.Domain.Repositories;
using ThesisProject.Infrastructure.Persistence.Models;

namespace ThesisProject.Infrastructure.Persistence.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly DatabaseContext _dbContext;

    public OrderRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Order order)
    {
        var dbModel = GetOrderModel(order);

        await _dbContext.AddAsync(dbModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid orderId)
    {
        var dbModel = await _dbContext.Orders
            .Where(e => e.Id == orderId)
            .SingleAsync();

        _dbContext.Orders.Remove(dbModel);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetById(Guid orderId)
    {
        var dbModel = await _dbContext.Orders
            .Include(e => e.OrderItems)
                .ThenInclude(e => e.Product)
            .Where(e => e.Id == orderId)
            .SingleOrDefaultAsync();

        if (dbModel is null)
        {
            return null;
        }

        var order = GetOrder(dbModel);
        return order;
    }
    public async Task Update(Order order)
    {
        var dbModel = await _dbContext.Orders
                    .Include(e => e.OrderItems)
                        .ThenInclude(e => e.Product)
                    .Where(e => e.Id == order.Id)
                    .SingleAsync();

        dbModel.Name = order.Name;
        dbModel.OrderStatusType = order.OrderStatus;
        dbModel.CalculatedPrice = order.Price;

        await SyncOrderItems(dbModel, order);

        await _dbContext.SaveChangesAsync();
    }

    private async Task SyncOrderItems(OrderModel orderModel, Order order)
    {
        var orderItemsForAdd = order.OrderItems
            .Where(e => !orderModel.OrderItems.Any(x => x.OrderId == order.Id && x.ProductId == e.Product.Id))
            .ToList();

        var orderItemsForUpdate = order.OrderItems
            .Where(e => orderModel.OrderItems.Any(x => x.OrderId == order.Id && x.ProductId == e.Product.Id))
            .ToList();

        var orderItemModelsForDelete = orderModel.OrderItems
            .Where(e => !order.OrderItems.Any(x => x.Product.Id == e.ProductId) && e.OrderId == order.Id)
            .ToList();

        foreach (var orderItemForAdd in orderItemsForAdd)
        {
            var orderItemModel = GetOrderItemModel(order.Id, orderItemForAdd);

            orderModel.OrderItems.Add(orderItemModel);
        }

        foreach (var orderItemForUpdate in orderItemsForUpdate)
        {
            var orderItemModel = orderModel.OrderItems.Single(e => e.OrderId == order.Id && e.ProductId == orderItemForUpdate.Product.Id);

            orderItemModel.Quantity = orderItemForUpdate.Quantity;
            orderItemModel.Price = orderItemForUpdate.CalculatedPrice;

        }

        foreach (var orderItemModel in orderItemModelsForDelete)
        {
            orderModel.OrderItems.Remove(orderItemModel);
        }
    }
    private OrderModel GetOrderModel(Order order)
    {
        var orderModel = new OrderModel
        {
            Id = order.Id,
            Name = order.Name,
            OrderStatusType = order.OrderStatus,
            OrderItems = order.OrderItems
            .Select(e => GetOrderItemModel(order.Id, e))
            .ToList()
        };

        return orderModel;
    }

    private OrderItemModel GetOrderItemModel(Guid orderId, OrderItem orderItem)
    {
        return new OrderItemModel
        {
            OrderId = orderId,
            ProductId = orderItem.Product.Id,
            Price = orderItem.CalculatedPrice,
            Quantity = orderItem.Quantity
        };
    }
    private Order GetOrder(OrderModel dbModel)
    {
        var order = new Order(dbModel.Id, dbModel.Name);

        foreach (var orderItemModel in dbModel.OrderItems)
        {
            var orderItem = GetOrderItem(orderItemModel);

            order.AddOrderItem(orderItem);
        }

        return order;
    }

    private OrderItem GetOrderItem(OrderItemModel orderItemModel)
    {
        var product = GetProduct(orderItemModel.Product);
        var orderItem = new OrderItem(product);

        orderItem.SetQuantity(orderItemModel.Quantity);

        return orderItem;
    }

    private Product GetProduct(ProductModel productModel)
    {
        var product = new Product(productModel.Id, productModel.Name, productModel.Price)
        {
            Description = productModel.Description
        };

        if (productModel.Stock > 0)
        {
            product.AddStock(productModel.Stock);
        }

        return product;
    }
}
