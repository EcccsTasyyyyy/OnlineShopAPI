using OnlineShopAPI.IRepository;
using OnlineShopAPI.Models;

namespace OnlineShopAPI.Service;


// IUserRpository - ის შვილობილობა აუცილებელია ? 
public class UserService : IUserRepository
{
    private readonly IUserRepository? _userRepository;

    private UserService(IUserRepository? userRepository)
    {
        _userRepository = userRepository;
    }

    public Task AddAsync(UserModel entity)
    {
        var user = new UserModel
        {
            UserName = entity.UserName,
            Password = entity.Password,
        };

        return _userRepository.AddAsync(user); //_userRepository may be null here.
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UserModel entity)
    {
        throw new NotImplementedException();
    }
}