using Microsoft.AspNetCore.Mvc;
using UdepEncuestas.Services;

namespace UdepEncuestas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("token")]
    public IActionResult Token([FromBody] string userId)
    {
        var accessToken = _authService.CreateToken(userId);
        var refreshToken = _authService.CreateRefreshToken(userId);
        return Ok(new { accessToken, refreshToken = refreshToken.Token });
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] string token)
    {
        var newAccessToken = _authService.UseRefreshToken(token);
        if (newAccessToken == null)
            return Unauthorized();
        return Ok(new { accessToken = newAccessToken });
    }
}
