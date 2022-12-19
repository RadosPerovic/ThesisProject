using ThesisProject.Domain.Common;
using ThesisProject.Domain.Exceptions;

namespace ThesisProject.Domain.Entities;
public class Product
{
    private Guid _id;
    private string _name;
    private decimal _price;

    public Product(
        Guid id,
        string name,
        decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public Guid Id
    {
        get { return _id; }
        private set
        {
            CommonGuard.GuidNotEmpty(nameof(Product), nameof(Id), value);
            _id = value;
        }
    }

    public string Name
    {
        get { return _name; }
        set
        {
            CommonGuard.StringNotNullOrEmpty(nameof(Product), nameof(Name), value);
            _name = value;
        }
    }

    public decimal Price
    {
        get { return _price; }
        set
        {
            CommonGuard.NumberNotZeroOrNegativeNumber(nameof(Product), nameof(Price), value);
            _price = value;
        }
    }
    public int Stock { get; private set; }
    public string? Description { get; set; }

    public void AddStock(int stock)
    {
        if (stock <= 0)
        {
            throw new DomainError("Stock cannot be sumed with 0 or less then 0 value");
        }

        Stock += stock;
    }

    public void DecreaseStock(int quantity)
    {
        Stock -= quantity;
    }
}
