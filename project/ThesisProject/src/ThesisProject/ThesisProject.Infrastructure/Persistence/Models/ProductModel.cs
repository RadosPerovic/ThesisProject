namespace ThesisProject.Infrastructure.Persistence.Models;
public class ProductModel : BaseModel<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public List<OrderItemModel> OrderItems { get; set; }
}
