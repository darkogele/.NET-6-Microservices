using Play.Common;

namespace Play.Catalog.Service.Entities;

public class Item : IEntity
{
    public Item(string name, string description, decimal price, DateTimeOffset createdDate)
    {
        Name = name;
        Description = description;
        Price = price;
        CreatedDate = createdDate;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}