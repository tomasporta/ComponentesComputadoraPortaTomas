using ComponentesComputadoras.Abstraccioness;
using ComponentesComputadoras.Entities;
using ComponentesComputadoras.Entities.MicrosoftIdentity;
using ComponentesComputadoras.Servicios.AuthServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComponentesComputadoras.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenHandlerService _tokenHandler;

        public AuthController(UserManager<User> userManager, ITokenHandlerService tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                //  Obtener roles del usuario
                var roles = await _userManager.GetRolesAsync(user);

                var parametros = new TokensParameters
                {
                    Id = user.Id.ToString(),
                    UserName = user.UserName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    Roles = roles // <-- propiedad nueva en TokensParameters
                };

                var token = _tokenHandler.GenerateJwtTokens(parametros);
                return Ok(new { Token = token });
            }
            return Unauthorized("Credenciales inválidas");
        }

    }

    public class LoginRequestDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

   
  
}
