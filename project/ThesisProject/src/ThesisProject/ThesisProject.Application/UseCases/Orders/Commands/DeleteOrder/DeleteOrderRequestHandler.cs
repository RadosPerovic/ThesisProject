using MediatR;
using ThesisProject.Application.Exceptions;
using ThesisProject.Domain.Entities;
using ThesisProject.Domain.Repositories;

namespace ThesisProject.Application.UseCases.Orders.Commands.DeleteOrder;
public class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderRequest, Unit>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderRequestHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(request.OrderId);

        ThrowIfOrderNotFound(request, order);

        await _orderRepository.Delete(request.OrderId);

        return Unit.Value;
    }

    private void ThrowIfOrderNotFound(DeleteOrderRequest request, Order? order)
    {
        if (order is null)
        {
            throw new ApplicationError($"Order with id {request.OrderId} not found");
        }
    }
}
