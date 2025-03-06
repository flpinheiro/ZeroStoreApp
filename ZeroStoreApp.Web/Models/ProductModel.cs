namespace ZeroStoreApp.Web.Models;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}

public class ProductListModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}

public class CreateProductModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class UpdateProductModel 
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class ProductRequest 
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Query { get; set; }
}