namespace ThesisProject.Infrastructure.Persistence.Models;
public class OrderItemModel
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public OrderModel Order { get; set; }
    public ProductModel Product { get; set; }
}
