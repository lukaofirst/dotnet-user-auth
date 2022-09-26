using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var entities = await _userRepository.GetAll();
            var mappedEntities = _mapper.Map<List<UserDTO>>(entities);

            return mappedEntities;
        }

        public async Task<UserDTO> FindByEmail(string email)
        {
            var entity = await _userRepository.FindByEmail(email);

            return _mapper.Map<UserDTO>(entity);
        }

        public async Task<UserDTO> InsertOne(UserDTO user)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password, salt);

            var parsedUser = new User
            {
                email = user.email,
                password = hashedPassword,
            };

            var entity = await _userRepository.InsertOne(parsedUser);
            var mappedEntity = _mapper.Map<UserDTO>(entity);

            return mappedEntity;
        }
    }
}
