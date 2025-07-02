namespace UdepEncuestas.Core.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public bool Revoked { get; set; }
}
