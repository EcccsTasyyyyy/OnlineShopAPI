using Microsoft.EntityFrameworkCore;

namespace OnlineShopAPI.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
}