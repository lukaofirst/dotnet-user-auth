using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> FindByEmail(string email);
        Task<UserDTO> InsertOne(UserDTO user);
    }
}
