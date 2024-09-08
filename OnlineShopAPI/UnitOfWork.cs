using OnlineShopAPI.Data;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Repository;

namespace OnlineShopAPI;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDbContext _dbContext;
    private IUserRepository? _userRepository;
    private ICategoryRepository? _categoryRepository;

    public UnitOfWork(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IUserRepository Users
    {
        get
        {
            return _userRepository??= new UserRepository(_dbContext);
        }
    }

    public ICategoryRepository Categories
    {
        get
        {
            return _categoryRepository??= new CategoryRepository(_dbContext);
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}