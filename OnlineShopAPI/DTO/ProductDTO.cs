using System.ComponentModel.DataAnnotations;

namespace OnlineShopAPI.DTO;

public class ProductDTO
{
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}