using ThesisProject.Domain.Enums;

namespace ThesisProject.Infrastructure.Persistence.Models;
public class OrderStatusModel : BaseModel<OrderStatus>
{
    public string Name { get; set; }

    public List<OrderModel> Orders { get; set; }
}
