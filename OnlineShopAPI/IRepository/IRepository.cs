namespace OnlineShopAPI.IRepository;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();

    //Task სა და Task<T> - ს შორის განსხვავება return type შია, პირველი არაფერს არ აბრუნებს და ამ სამ მეთოდს არაფრის დაბრუნება - 
            //სჭირდებათ, ხოლო მეორე აბრუნებს entity - ს, ამ ორ მეთოდის ფუნქციაც ეგაა.
}