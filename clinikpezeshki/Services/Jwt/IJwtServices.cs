using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

public interface IJwtServices
{
    IEnumerable<Claim> GetAccessClaim(JwtRoles[] roles);
    CookieOptions GetCookieOptions();
    string GetJwtToken(IEnumerable<Claim> claims);
    Task<string> GetJwtTokenAsync(IEnumerable<Claim> claims, CancellationToken cancellationToken);
    bool HaveClientClaim(IEnumerable<Claim> accessClaim, IEnumerable<Claim> clientClaim);
    SecurityToken? ValidateToken(string? token);

    IEnumerable<Claim>? GetClaims(string? securityToken);
}