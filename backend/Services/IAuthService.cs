using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUser(RegisterDto registerDto);

        Task<AuthResponseDto> LoginUser(LoginDto loginDto);
    }
}