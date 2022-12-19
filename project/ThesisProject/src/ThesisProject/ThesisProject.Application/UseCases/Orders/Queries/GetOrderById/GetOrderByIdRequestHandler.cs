using MediatR;

namespace ThesisProject.Application.UseCases.Orders.Queries.GetOrderById;
public class GetOrderByIdRequestHandler : IRequestHandler<GetOrderByIdRequest, OrderDto?>
{
    private readonly IGetOrderByIdQuery _getOrderByIdQuery;

    public GetOrderByIdRequestHandler(IGetOrderByIdQuery getOrderByIdQuery)
    {
        _getOrderByIdQuery = getOrderByIdQuery;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var order = await _getOrderByIdQuery.Execute(request.OrderId);

        return order;
    }
}
