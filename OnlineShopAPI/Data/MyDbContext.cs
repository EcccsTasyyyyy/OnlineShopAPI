using Microsoft.EntityFrameworkCore;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Data;

public class MyDbContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
}