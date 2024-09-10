namespace OnlineShopAPI.DTO;

public class ProductCreationDTO
{
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int CategoryId { get; set; }
}