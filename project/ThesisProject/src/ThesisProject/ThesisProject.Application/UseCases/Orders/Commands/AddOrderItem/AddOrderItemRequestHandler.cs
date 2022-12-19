using MediatR;
using ThesisProject.Application.Exceptions;
using ThesisProject.Domain.Entities;
using ThesisProject.Domain.Repositories;

namespace ThesisProject.Application.UseCases.Orders.Commands.AddOrderItem;
public class AddOrderItemRequestHandler : IRequestHandler<AddOrderItemRequest, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public AddOrderItemRequestHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(AddOrderItemRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(request.OrderId);
        ThrowIfOrderNotFound(request, order);
        ThrowIdOrderIsPlaced(order!);

        var product = await _productRepository.GetById(request.ProductId);
        ThrowIfProductNotFound(request, product);

        ThrowIfProductExistsInOrderItem(request, order, product);

        var orderItem = CreateOrderItem(request, product);
        order!.AddOrderItem(orderItem);

        await _orderRepository.Update(order);
        await UpdateProductInDatabase(order);

        return Unit.Value;
    }

    private async Task UpdateProductInDatabase(Order order)
    {
        var products = order.OrderItems
            .Select(e => e.Product)
            .ToList();

        foreach (var product in products)
        {
            await _productRepository.Update(product);
        }
    }
    private OrderItem CreateOrderItem(AddOrderItemRequest request, Product product)
    {
        var orderItem = new OrderItem(product);
        orderItem.SetQuantity(request.Quantity);

        return orderItem;
    }

    private void ThrowIfProductExistsInOrderItem(AddOrderItemRequest request, Order? order, Product product)
    {
        if (order.OrderItems.Select(e => e.Product).Contains(product))
        {
            throw new ApplicationError($"Product with id {request.ProductId} already exists in Order with id {request.OrderId}");
        }
    }

    private void ThrowIdOrderIsPlaced(Order order)
    {
        if (order.OrderStatus == Domain.Enums.OrderStatus.Placed)
        {
            throw new ApplicationError($"Order with id {order.Id} cannot be updated beacase it's placed.");
        }
    }

    private void ThrowIfProductNotFound(AddOrderItemRequest request, Product? product)
    {
        if (product is null)
        {
            throw new ApplicationError($"Product with id {request.ProductId} not found");
        }
    }

    private void ThrowIfOrderNotFound(AddOrderItemRequest request, Order? order)
    {
        if (order is null)
        {
            throw new ApplicationError($"Order with id {request.OrderId} not found");
        }
    }
}
