using Microsoft.EntityFrameworkCore;
using OnlineShopAPI.Data;
using OnlineShopAPI.DTO;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly MyDbContext _context;

    public ProductRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ProductModel entity)
    {
        try
        {
            await _context.AddAsync(entity);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Can`t add Product", ex.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new ArgumentException($"Can`t find Product by ID: {id}");
            }
            _context.Products.Remove(product);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("unexpected error, Product can`t be deleted", ex.Message);
        }
    }

    public async Task<IEnumerable<ProductModel>> GetAllAsync()
    {
        try
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Error while retrieving data", ex.Message);
        }
    }

    public async Task<ProductModel> GetByIdAsync(int id)
    {
        try
        {
            var result = await _context.Products.Include(p => p.Category)
                                                .FirstOrDefaultAsync(p => p.Id == id);

            if (result == null)
            {
                throw new ArgumentException($"Can`t find Product by ID: {id}");
            }
            return result;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Unexpected error while retrieving data", ex.Message);
        }
    }

    public async Task UpdateAsync(ProductModel entity)
    {
        try
        {
            var product = await GetByIdAsync(entity.Id);

            if (product == null)
            {
                throw new ArgumentException($"Can`t find Product by ID:{entity.Id}");
            }

            product.Name = entity.Name;
            product.Price = entity.Price;
            product.CategoryId = entity.CategoryId; // პროდუქტისთვის კატეგორიის შეცვლა ასე მოხდება ?
        }
        catch(Exception ex)
        {
            throw new ArgumentException($"Can`t update database", ex.Message);
        }
    }
}