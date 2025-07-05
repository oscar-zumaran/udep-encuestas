using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace UDEP.Encuestas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public AuthController(IDbConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }

        public record LoginRequest(string Username, string Password);

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _connection.QuerySingleOrDefaultAsync<dynamic>(
                "SELECT * FROM Usuario WHERE cUsuario = @u AND cPasswordHash = @p AND bActive = 1",
                new { u = request.Username, p = request.Password });

            if (user == null)
                return Unauthorized();

            var roles = await _connection.QueryAsync<string>(@"SELECT R.cNombreRol
                                                                FROM Rol R
                                                                INNER JOIN Usuario_Rol UR ON R.iIdRol = UR.iIdRol
                                                                WHERE UR.iIdUsuario = @id AND UR.bActive = 1 AND R.bActive = 1",
                new { id = user.iIdUsuario });

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Username) };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
