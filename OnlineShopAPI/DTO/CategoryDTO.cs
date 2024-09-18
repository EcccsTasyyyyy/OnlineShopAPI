using System.ComponentModel.DataAnnotations;

namespace OnlineShopAPI.DTO;

public class CategoryDTO
{
    [Required]
    public string? CategoryName { get; set; }
}