using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UdepEncuestas.Core.Models;
using UdepEncuestas.Data;

namespace UdepEncuestas.Services;

public class AuthService
{
    private readonly DepartmentRepository _repository; // sample dependency
    private readonly SymmetricSecurityKey _key;
    private readonly List<RefreshToken> _tokens = new();

    public AuthService(DepartmentRepository repository, string jwtSecret)
    {
        _repository = repository;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
    }

    public string CreateToken(string userId)
    {
        var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, userId) };
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(1),
            claims: claims,
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken CreateRefreshToken(string userId)
    {
        var refresh = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            UserId = userId,
            Expiration = DateTime.UtcNow.AddDays(7)
        };
        _tokens.Add(refresh);
        return refresh;
    }

    public string? UseRefreshToken(string token)
    {
        var refresh = _tokens.FirstOrDefault(t => t.Token == token && !t.Revoked && t.Expiration > DateTime.UtcNow);
        if (refresh == null) return null;
        refresh.Revoked = true;
        return CreateToken(refresh.UserId);
    }
}
