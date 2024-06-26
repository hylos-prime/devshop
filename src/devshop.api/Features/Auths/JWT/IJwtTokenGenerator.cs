using System.Security.Claims;

namespace devshop.api.Features.Auths.JWT;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(
        Guid id, 
        string userName, 
        string email, 
        IEnumerable<Claim> permissions);
}