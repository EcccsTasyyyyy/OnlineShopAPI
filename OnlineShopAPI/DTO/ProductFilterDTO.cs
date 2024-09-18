namespace OnlineShopAPI.DTO;

public class ProductFilterDTO
{
    public string? ProductName { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? CategoryId { get; set; }
}