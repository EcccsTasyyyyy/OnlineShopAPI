using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet("categorys")]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var result = await _categoryRepository.GetAllAsync();
            return Ok(result);
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
            var result = await _categoryRepository.GetByIdAsync(id);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryModel category)
    {
        try
        {
            category = new CategoryModel()
            {
                CategoryName = category.CategoryName
            };

            await _categoryRepository.AddAsync(category);
            return Ok($"Category created successfully: {category}");
        }
        catch(Exception ex )
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryModel category)
    {
        try
        {
            await _categoryRepository.UpdateAsync(category);
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
            await _categoryRepository.DeleteAsync(id);
            return Ok("Category deleted successfully");
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}