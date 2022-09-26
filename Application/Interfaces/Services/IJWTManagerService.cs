using Application.DTOs;
using Core.Entities;

namespace Application.Interfaces.Services
{
    public interface IJWTManagerService
    {
        Task<Token> Authenticate(UserDTO user);
    }
}
