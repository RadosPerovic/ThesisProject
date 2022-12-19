using ThesisProject.Domain.Enums;

namespace ThesisProject.Application.UseCases.Orders.Queries.GetOrderById;
public class OrderDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<OrderItemDto> OrderItems { get; set; }
}

public class OrderItemDto
{
    public ProductDto Product { get; set; }
    public int Quantity { get; set; }
    public decimal CalculatedPrice { get; set; }
}

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
