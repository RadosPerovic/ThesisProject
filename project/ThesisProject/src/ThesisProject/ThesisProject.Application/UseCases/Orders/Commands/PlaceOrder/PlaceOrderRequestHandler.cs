using MediatR;
using ThesisProject.Application.Exceptions;
using ThesisProject.Domain.Entities;
using ThesisProject.Domain.Repositories;

namespace ThesisProject.Application.UseCases.Orders.Commands.PlaceOrder;
public class PlaceOrderRequestHandler : IRequestHandler<PlaceOrderRequest, Unit>
{
    private readonly IOrderRepository _orderRepository;

    public PlaceOrderRequestHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(PlaceOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(request.OrderId);
        ThrowIfOrderNotFound(request, order);

        ThrowIdOrderIsPlaced(order!);

        order!.PlaceOrder();

        await _orderRepository.Update(order);

        return Unit.Value;
    }

    private void ThrowIfOrderNotFound(PlaceOrderRequest request, Order? order)
    {
        if (order is null)
        {
            throw new ApplicationError($"Order with id {request.OrderId} not found");
        }
    }

    private void ThrowIdOrderIsPlaced(Order order)
    {
        if (order.OrderStatus == Domain.Enums.OrderStatus.Placed)
        {
            throw new ApplicationError($"Order with id {order.Id} cannot be updated beacase it's placed.");
        }
    }
}
