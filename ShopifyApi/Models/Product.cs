namespace ShopifyApi.Models;

public class Product
{
    public int Id { get; set; } // Primary key 
    public string Name { get; set; } = string.Empty; // string.empty prevents null warning
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}