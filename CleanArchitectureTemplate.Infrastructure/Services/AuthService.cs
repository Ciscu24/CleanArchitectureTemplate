using CleanArchitectureTemplate.Application.DTOs.Auth;
using CleanArchitectureTemplate.Application.Services;
using CleanArchitectureTemplate.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitectureTemplate.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> Register(RegisterDto request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                return new AuthResponseDto { Success = false, Message = "El usuario ya existe" };

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email, // Identity suele requerir un UserName, usamos el email
                Name = request.Name
            };

            // CreateAsync cifra la contraseña automáticamente y guarda en BD
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new AuthResponseDto { Success = false, Message = "Error al crear usuario" };

            return new AuthResponseDto { Success = true, Message = "Usuario creado con éxito" };
        }

        public async Task<AuthResponseDto> Login(LoginDto request)
        {
            // 1. Verificamos que el usuario exista
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new AuthResponseDto { Success = false, Message = "Credenciales inválidas" };

            // 2. Comprobamos la contraseña
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                return new AuthResponseDto { Success = false, Message = "Credenciales inválidas" };

            // 3. Si todo es correcto, generamos el Token JWT
            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                Message = "Login exitoso"
            };
        }

        // Método privado para fabricar el JWT
        private string GenerateJwtToken(ApplicationUser user)
        {
            // Los Claims son los "datos" que viajan dentro del token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("Name", user.Name) // Claim personalizado
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
