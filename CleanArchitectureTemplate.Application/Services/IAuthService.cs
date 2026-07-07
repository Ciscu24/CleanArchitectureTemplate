using CleanArchitectureTemplate.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureTemplate.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Register(RegisterDto request);
        Task<AuthResponseDto> Login(LoginDto request);
    }
}