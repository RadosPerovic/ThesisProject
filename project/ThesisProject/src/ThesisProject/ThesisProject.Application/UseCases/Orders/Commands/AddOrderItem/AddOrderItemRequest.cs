using MediatR;

namespace ThesisProject.Application.UseCases.Orders.Commands.AddOrderItem;
public class AddOrderItemRequest : IRequest<Unit>
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
