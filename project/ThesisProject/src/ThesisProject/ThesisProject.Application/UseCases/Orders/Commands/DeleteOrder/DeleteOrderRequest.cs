using MediatR;

namespace ThesisProject.Application.UseCases.Orders.Commands.DeleteOrder;
public class DeleteOrderRequest : IRequest<Unit>
{
    public Guid OrderId { get; set; }
}
