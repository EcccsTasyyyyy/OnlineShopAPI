using Microsoft.EntityFrameworkCore;
using OnlineShopAPI.Data;
using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;

    public UserRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(UserModel entity)
    {
        try
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Cant`t add User", ex.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException($"Can`t find user by ID: {id}");
            }

            _context.Users.Remove(user); // აქ await არ დამაწერინა, მეჩხუბა
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ArgumentException("unexpected error, User can`t be deleted", ex.Message);
        }
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Error while retrieving data", ex.Message);
        }
    }

    public async Task<UserModel> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Users.FindAsync(id); // ამას რავუყო რო ნალი არ იყოს?
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Can`t find user by ID: {id}", ex.Message);
        }
    }

    public async Task UpdateAsync(UserModel entity)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == entity.Id);

            if (user == null)
            {
                throw new ArgumentException($"Can't find user with ID: {entity.Id}");
            }

            user.UserName = entity.UserName;
            user.Password = entity.Password;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Cant`t update database", ex.Message);
        }
    }
}
