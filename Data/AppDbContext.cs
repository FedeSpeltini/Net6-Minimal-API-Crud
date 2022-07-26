using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Book> Books => Set<Book>();

}