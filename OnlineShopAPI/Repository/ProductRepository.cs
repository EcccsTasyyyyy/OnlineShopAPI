using Microsoft.EntityFrameworkCore;
using OnlineShopAPI.Data;
using OnlineShopAPI.DTO;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;
using System.Reflection;

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

            product.ProductName = entity.ProductName;
            product.UnitPrice = entity.UnitPrice;
            product.CategoryId = entity.CategoryId;
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Can`t update database", ex.Message);
        }
    }

    public async Task<IEnumerable<ProductModel>> FilteredProducts(ProductFilterDTO filterDTO)
    {
        var query = _context.Products.Include(p => p.Category).AsQueryable();

        if (!string.IsNullOrEmpty(filterDTO.ProductName))
        {
            query = query.Where(p => p.ProductName.Contains(filterDTO.ProductName));
        }

        if (filterDTO.MinPrice.HasValue)
        {
            query = query.Where(p => p.UnitPrice >= filterDTO.MinPrice.Value);
        }

        if (filterDTO.MaxPrice.HasValue)
        {
            query = query.Where(p => p.UnitPrice <= filterDTO.MaxPrice.Value);
        }

        if (filterDTO.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == filterDTO.CategoryId.Value);
        }

        return await query.ToListAsync();
    }

    public IEnumerable<ProductModel> SortProducts(SortingDTO sortDTO, IEnumerable<ProductModel> products)
    {
        if(string.IsNullOrEmpty(sortDTO.SortBy))
        {
            return products;
        }

        var productInfo = typeof(ProductModel).GetProperty(sortDTO.SortBy);
        if(productInfo == null)
        {
            throw new ArgumentException($"Invalid sort field: {sortDTO.SortBy}");
        }

        return sortDTO.IsAscending
            ? products.OrderBy(p => productInfo.GetValue(p, null)).ToList()
            : products.OrderByDescending(p => productInfo.GetValue(p, null)).ToList();
    }

    public IEnumerable<ProductModel> PaginateProducts(PaginationDTO paginationDTO, IEnumerable<ProductModel> products)
    {
        products = products.Skip((paginationDTO.PageNumber - 1) * paginationDTO.PageSize)
            .Take(paginationDTO.PageSize)
            .ToList();

        return products;
    }
}