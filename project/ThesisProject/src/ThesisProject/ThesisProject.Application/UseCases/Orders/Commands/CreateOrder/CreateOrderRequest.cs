using MediatR;

namespace ThesisProject.Application.UseCases.Orders.Commands.CreateOrder;
public class CreateOrderRequest : IRequest<CreateOrderResponse>
{
    public string Name { get; set; }
}
