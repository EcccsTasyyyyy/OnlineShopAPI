namespace OnlineShopAPI.Models;

public class CategoryModel
{
    public int Id { get; set; }
    public string? CategoryName { get; set; }
    public ICollection<ProductModel>? Products { get; set; }
}