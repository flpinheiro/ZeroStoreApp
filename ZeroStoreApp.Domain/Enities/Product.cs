namespace ZeroStoreApp.Domain.Enities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public virtual decimal CalculateDiscount(int quantity)
    {
        if (quantity < 4) return 0;
        else if (quantity <= 10) return 5;
        else if (quantity <= 20) return 10;
        else return 15;
    }

    public virtual void Update(Product product)
    {
        this.Name = product.Name;
        this.Description = product.Description;
        this.Price = product.Price;
        this.Stock = product.Stock;
    }
}
