using Microsoft.EntityFrameworkCore;
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

    public async Task AddAsync(CategoryModel entity)
    {
        try
        {
            await _context.AddAsync(entity);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Can`t add Category", ex.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                throw new ArgumentException($"Can't find Category by ID: {id}");
            }

            _context.Categories.Remove(category);
        }
        catch(Exception ex)
        {
            throw new ArgumentException("unexpected error, Category can`t be deleted", ex.Message);
        }
    }

    public async Task<IEnumerable<CategoryModel>> GetAllAsync()
    {
        try
        {
            return await _context.Categories.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Error while retrieving data", ex.Message);
        }
    }

    public async Task<CategoryModel> GetByIdAsync(int id)
    {
        try
        {
            var result = await _context.Categories.FindAsync(id);

            if (result == null)
            {
                throw new ArgumentException($"Can`t find Category by ID: {id}");
            }
            return result;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Unexpected error while retrieving data", ex.Message);
        }
    }

    public async Task UpdateAsync(CategoryModel entity)
    {
        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == entity.Id);

            if(category == null )
            {
                throw new ArgumentException($"Cant`t find Category by ID: {entity.Id}");
            }

            category.CategoryName = entity.CategoryName;
        }
        catch(Exception ex)
        {
            throw new ArgumentException($"Can`t update database",ex.Message);
        }
    }
}