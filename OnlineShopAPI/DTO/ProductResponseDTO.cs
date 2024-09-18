using System.ComponentModel.DataAnnotations;

namespace OnlineShopAPI.DTO;

public class ProductResponseDTO
{
    public int Id { get; set; }
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}