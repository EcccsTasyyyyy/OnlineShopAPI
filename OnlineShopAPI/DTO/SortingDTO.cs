namespace OnlineShopAPI.DTO;

public class SortingDTO
{
    public string? SortBy { get; set; }
    public bool IsAscending { get; set; } = true; // default to ascending order
}