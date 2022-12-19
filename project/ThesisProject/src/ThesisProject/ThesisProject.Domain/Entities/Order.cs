using ThesisProject.Domain.Common;
using ThesisProject.Domain.Enums;

namespace ThesisProject.Domain.Entities;
public class Order
{
    private Guid _id;
    private string _name;
    private readonly IList<OrderItem> _orderItems;

    public Order(Guid id, string name)
    {
        Id = id;
        Name = name;

        OrderStatus = OrderStatus.Created;
        _orderItems = new List<OrderItem>();
    }

    public Guid Id
    {
        get { return _id; }
        private set
        {
            CommonGuard.GuidNotEmpty(nameof(Order), nameof(Id), value);
            _id = value;
        }
    }

    public string Name
    {
        get { return _name; }
        private set
        {
            CommonGuard.StringNotNullOrEmpty(nameof(Order), nameof(Name), value);
            _name = value;
        }
    }

    public OrderStatus OrderStatus { get; set; }
    public decimal Price { get; private set; }
    public IEnumerable<OrderItem> OrderItems
    {
        get { return _orderItems; }
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);

        CalculatePrice();
    }

    public void PlaceOrder()
    {
        OrderStatus = OrderStatus.Placed;
    }

    public void UpdateOrderItemQuantity(Guid productId, int quantity)
    {
        var orderItem = _orderItems.Single(e => e.Product.Id == productId);

        orderItem.AddQuantity(quantity);
        CalculatePrice();
    }

    private void CalculatePrice()
    {
        Price = 0;

        foreach (var order in _orderItems)
        {
            Price += order.CalculatedPrice;
        }
    }
}
