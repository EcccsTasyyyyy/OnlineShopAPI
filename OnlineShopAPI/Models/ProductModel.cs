namespace OnlineShopAPI.Models;

public class ProductModel
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int CategoryId { get; set; }
    public CategoryModel? Category { get; set; }
}