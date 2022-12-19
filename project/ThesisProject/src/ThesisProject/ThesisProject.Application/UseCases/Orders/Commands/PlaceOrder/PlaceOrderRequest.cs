using MediatR;

namespace ThesisProject.Application.UseCases.Orders.Commands.PlaceOrder;
public class PlaceOrderRequest : IRequest<Unit>
{
    public Guid OrderId { get; set; }
}
