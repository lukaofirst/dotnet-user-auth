using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IJWTManagerService
    {
        Task<Token> Authenticate(User user);
    }
}
