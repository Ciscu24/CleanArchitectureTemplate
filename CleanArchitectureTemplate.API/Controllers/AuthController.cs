using CleanArchitectureTemplate.Application.DTOs.Auth;
using CleanArchitectureTemplate.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tappa.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // La ruta será: api/auth
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Inyectamos la abstracción de la capa de Application
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.Register(request);

            if (!response.Success)
            {
                // Devolvemos un HTTP 400 con el mensaje de error ("El usuario ya existe", etc.)
                return BadRequest(response);
            }

            // HTTP 200 OK
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.Login(request);

            if (!response.Success)
            {
                // Devolvemos un HTTP 401 Unauthorized si falla la contraseña o el email
                return Unauthorized(response);
            }

            // HTTP 200 OK con el Token dentro
            return Ok(response);
        }
    }
}
