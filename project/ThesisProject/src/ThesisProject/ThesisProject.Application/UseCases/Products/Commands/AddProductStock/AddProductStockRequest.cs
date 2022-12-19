using MediatR;

namespace ThesisProject.Application.UseCases.Products.Commands.AddProductStock;
public class AddProductStockRequest : IRequest<Unit>
{
    public Guid ProjectId { get; set; }
    public int Stock { get; set; }
}
