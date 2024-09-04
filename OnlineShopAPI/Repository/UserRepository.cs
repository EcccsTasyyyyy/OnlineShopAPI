using OnlineShopAPI.Data;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext? _context;

    public UserRepository(MyDbContext? context)
    {
        _context = context;
    }
    public async Task AddAsync(UserModel entity)
    {
        await _context.AddAsync(entity); // _context may be null here რატო მიწერს.
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
    }

    public Task<UserModel> GetByIdAsync(int id)
    {
    }

    public Task UpdateAsync(UserModel entity)
    {
    }
}
