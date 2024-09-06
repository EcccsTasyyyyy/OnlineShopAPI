using OnlineShopAPI.Data;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly MyDbContext _context;

    public CategoryRepository(MyDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(CategoryModel entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategoryModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CategoryModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CategoryModel entity)
    {
        throw new NotImplementedException();
    }
}