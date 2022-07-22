﻿using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infraestructure.Services
{
    public class JWTManagerService : IJWTManagerService
    {
        private readonly IOptions<JWTConfig> _jwtConfig;
        private readonly IUserRepository _userRepository;

        public JWTManagerService(IOptions<JWTConfig> jwtConfig, IUserRepository userRepository)
        {
            _jwtConfig = jwtConfig;
            _userRepository = userRepository;
        }

        public async Task<Token> Authenticate(User user)
        {
            var userExist = await _userRepository.FindByEmail(user.email!);

            if (userExist == null || !BCrypt.Net.BCrypt.Verify(user.password, userExist.password)) 
            {
                throw new Exception("No user found");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.SetDefaultTimesOnTokenCreation = false;
            var tokenKey = Encoding.UTF8.GetBytes(_jwtConfig.Value.Key!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                    new Claim(ClaimTypes.Email, user.email!)
                  }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Token { token = tokenHandler.WriteToken(token) };
        }
    }
}
