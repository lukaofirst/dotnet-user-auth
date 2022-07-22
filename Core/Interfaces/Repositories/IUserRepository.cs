using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> FindByEmail(string email);
        Task<User> InsertOne(User user);
    }
}
