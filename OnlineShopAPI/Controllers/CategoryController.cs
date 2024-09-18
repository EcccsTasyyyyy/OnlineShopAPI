using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.DTO;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            var categoryResponseDTO = categories.Select(c => new CategoryResponseDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
            }).ToList();

            return Ok(categoryResponseDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500,$"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        try
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            var categoryResponseDTO = new CategoryResponseDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };

            return Ok(categoryResponseDTO);
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            var category = new CategoryModel()
            {
                CategoryName = categoryDTO.CategoryName
            };

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return Ok($"Category created successfully: {category}");
        }
        catch(Exception ex )
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCategory(int id,[FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            var categoryUpdate = await _unitOfWork.Categories.GetByIdAsync(id);

            categoryUpdate.CategoryName = categoryDTO.CategoryName;

            await _unitOfWork.Categories.UpdateAsync(categoryUpdate);
            await _unitOfWork.SaveChangesAsync();
            return Ok($"Category updated successfully");
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Category deleted successfully");
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}