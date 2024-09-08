namespace OnlineShopAPI.IRepository;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    Task<int> SaveChangesAsync();
}