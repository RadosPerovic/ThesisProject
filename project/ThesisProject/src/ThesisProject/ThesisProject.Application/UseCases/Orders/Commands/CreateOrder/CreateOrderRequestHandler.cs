using MediatR;
using ThesisProject.Application.Services;
using ThesisProject.Domain.Entities;
using ThesisProject.Domain.Repositories;

namespace ThesisProject.Application.UseCases.Orders.Commands.CreateOrder;
public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IIdentityGenerator _identityGenerator;

    public CreateOrderRequestHandler(IOrderRepository orderRepository, IIdentityGenerator identityGenerator)
    {
        _orderRepository = orderRepository;
        _identityGenerator = identityGenerator;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var orderId = _identityGenerator.GenerateGuidId();

        var order = new Order(orderId, request.Name);

        await _orderRepository.Add(order);

        return new CreateOrderResponse
        {
            CreatedId = orderId,
        };
    }
}
