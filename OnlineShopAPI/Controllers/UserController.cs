using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;
using OnlineShopAPI.Repository;

namespace OnlineShopAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var result = await _userRepository.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var result = await _userRepository.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserModel user)
    {
        try
        {
            user = new UserModel()
            {
                UserName = user.UserName,
                Password = user.Password,
            };
            await _userRepository.AddAsync(user);
            return Ok("User created successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser(UserModel user)
    {
        try
        {
            await _userRepository.UpdateAsync(user);
            return Ok("User updated successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await _userRepository.DeleteAsync(id);
            return Ok("User deleted successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}