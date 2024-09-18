using OnlineShopAPI.DTO;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.IRepository;

public interface IProductRepository : IRepository<ProductModel>
{
    //Task<IEnumerable<ProductModel>> FilteredProducts(ProductFilterDTO filterDTO);
    IEnumerable<ProductModel> SortProducts(SortingDTO sortDTO, IEnumerable<ProductModel> products);
    IEnumerable<ProductModel> PaginateProducts(PaginationDTO paginationDTO, IEnumerable<ProductModel> products);
}