﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.DTO;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            var productResponseDTO = products.Select(p => new ProductResponseDTO
            {
                Id = p.Id,
                ProductName = p.Name,
                UnitPrice = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.CategoryName
            }).ToList();

            return Ok(productResponseDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            var productResponseDTO = new ProductResponseDTO
            {
                Id = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.CategoryName
            };

            return Ok(productResponseDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
    {
        try
        {
            var product = new ProductModel
            {
                Name = productDTO.ProductName,
                Price = productDTO.UnitPrice,
                CategoryId = productDTO.CategoryId
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return Ok($"Product created successfully: {product}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
    {
        try
        {
            var productUpdate = await _unitOfWork.Products.GetByIdAsync(id);

            productUpdate.Name = productDTO.ProductName;
            productUpdate.Price = productDTO.UnitPrice;
            productUpdate.CategoryId = productDTO.CategoryId;

            await _unitOfWork.Products.UpdateAsync(productUpdate);
            await _unitOfWork.SaveChangesAsync();

            return Ok($"Product updated successfully\n{productUpdate}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Product deleted successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}