using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;
using backend.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration){
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterUser(RegisterDto registerDto){
            if (_userRepository.GetUserByUsernameAsync(registerDto.Username) == null ){
                throw new Exception("Username is existed.");
            }
            if (_userRepository.GetUserByEmailAsync(registerDto.Email) == null ){
                throw new Exception("Email is existed.");
            }

            CreateHashPassword(registerDto.Password, out string PasswordHash, out string PasswordSalt);

            var user = new User{
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
                CreatedAt = DateTime.UtcNow
            };

            int userId = await _userRepository.CreateUserAsync(user);
            user.Id = userId;
            
            string token = GenerateJwtToken(user);
            return new  AuthResponseDto{
                UserId = user.Id,
                Username = user.Username,
                Token = token
            };
        }

        public async Task<AuthResponseDto> LoginUser(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
            if (user == null)
            {
                throw new Exception("Invalid username or password");
            }

            if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Invalid username or password");
            }

            string token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                Token = token
            };
        }

        private void CreateHashPassword(string password, out string PasswordHash, out string PasswordSalt){
            byte[] saltBytes = new byte[128/8];

            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create()){
                rng.GetBytes(saltBytes);
            }

            PasswordSalt = Convert.ToBase64String(saltBytes);

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password + PasswordSalt);
        }

        private bool VerifyPasswordHash(string password, string storeHash, string storeSalt){
            return BCrypt.Net.BCrypt.Verify(password + storeSalt, storeHash);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}