using OnlineShopAPI.Data;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Repository;

namespace OnlineShopAPI;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDbContext _dbContext;
    private IUserRepository? _userRepository;
    private ICategoryRepository? _categoryRepository;
    private IProductRepository? _productRepository;

    public UnitOfWork(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //null-coalescing assignment operator. if the value on the left side is null it assigns the value from the right side.
    //if the left is not null, no assignment happens
    public IUserRepository Users
    {
        get
        {
            return _userRepository ??= new UserRepository(_dbContext);
        }
    }

    public ICategoryRepository Categories
    {
        get
        {
            return _categoryRepository ??= new CategoryRepository(_dbContext);
        }
    }

    public IProductRepository Products
    {
        get
        {
            return _productRepository ??= new ProductRepository(_dbContext);
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