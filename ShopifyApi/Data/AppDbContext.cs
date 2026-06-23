using Microsoft.EntityFrameworkCore;
using ShopifyApi.Models;

namespace ShopifyApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Product> Products { get; set; }
}

