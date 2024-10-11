namespace TEAMLIFTSS.Authorization;

public class JwtSettings
{
    public string? JwtSecurityKey { get; set; }
}

public class TokenResponse
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}