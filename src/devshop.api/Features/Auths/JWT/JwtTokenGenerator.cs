using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using devshop.api.Cores.Utilities;
using devshop.api.Features.Auths.Securities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace devshop.api.Features.Auths.JWT;

public class JwtTokenGenerator(
    IDateTimeProvider dateTimeProvider,
    IOptions<JwtSettings> jwtSettings) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GenerateJwtToken(
        Guid id, 
        string userName, 
        string email,
        IEnumerable<Claim> permissions)
    {
        var jti = Guid.NewGuid().ToString();
        var tokenHandler = new JwtSecurityTokenHandler();

        var signInCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Jti, jti),
            new(JwtRegisteredClaimNames.Sub, userName),
        };
        
        claims.AddRange(permissions
            .Where(x => x.Value.Equals("true"))
            .Select(permission => new Claim(DevshopClaims.ClaimType, permission.Type)));

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinute),
            SigningCredentials = signInCredentials,
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            IssuedAt = dateTimeProvider.UtcNow,
        };

        var token = tokenHandler.CreateToken(securityTokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}