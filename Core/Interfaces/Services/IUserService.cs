using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> FindByEmail(string email);
        Task<User> InsertOne(User user);
    }
}
