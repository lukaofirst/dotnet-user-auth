using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infraestructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _userRepository.FindByEmail(email);
        }

        public async Task<User> InsertOne(User user)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password, salt);

            var parsedUser = new User
            {
                email = user.email,
                password = hashedPassword,
            };

            var entity = await _userRepository.InsertOne(parsedUser);

            return entity;
        }
    }
}
