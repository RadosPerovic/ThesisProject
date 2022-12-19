using ThesisProject.Domain.Enums;

namespace ThesisProject.Infrastructure.Persistence.Models;
public class OrderModel : BaseModel<Guid>
{
    public string Name { get; set; }
    public OrderStatus OrderStatusType { get; set; }
    public decimal CalculatedPrice { get; set; }

    public OrderStatusModel OrderStatus { get; set; }
    public List<OrderItemModel> OrderItems { get; set; }
}
