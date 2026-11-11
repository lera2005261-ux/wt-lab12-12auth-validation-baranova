using Microsoft.EntityFrameworkCore;

using wt_lab12_12auth_validation_baranova.Models;
namespace wt_lab12_12auth_validation_baranova.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
