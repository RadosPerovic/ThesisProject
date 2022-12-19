using ThesisProject.Domain.Common;
using ThesisProject.Domain.Exceptions;

namespace ThesisProject.Domain.Entities;
public class OrderItem
{
    private Product _product;
    private int _quantity;

    public OrderItem(Product product)
    {
        Product = product;
    }

    public Product Product
    {
        get { return _product; }
        private set
        {
            CommonGuard.ObjectNotNull(nameof(OrderItem), nameof(Product), value);
            _product = value;
        }
    }

    public int Quantity
    {
        get { return _quantity; }
        private set
        {
            CommonGuard.NumberNotZeroOrNegativeNumber(nameof(OrderItem), nameof(Quantity), value);
            _quantity = value;
        }
    }

    public decimal CalculatedPrice { get; set; }

    private void CalculatePrice()
    {
        CalculatedPrice = _product.Price * Quantity;
    }

    public void SetQuantity(int quantity)
    {
        if (Product.Stock < quantity)
        {
            throw new DomainError($"Product with id {Product.Id} is out of stock.");
        }

        Quantity = quantity;

        CalculatePrice();
        Product.DecreaseStock(quantity);
    }

    public void AddQuantity(int quantity)
    {
        Quantity = quantity;

    }
}
