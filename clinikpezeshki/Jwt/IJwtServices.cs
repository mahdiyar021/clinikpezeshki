using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

public interface IJwtServices
{
    IEnumerable<Claim> GetAccessClaim(JwtRoles[] roles);
    Task<CookieOptions> GetCookieOptionsAsync();
    Task<CookieOptions> GetCookieOptionsAsync(CancellationToken cancellationToken);
    string GetJwtToken(IEnumerable<Claim> claims);
    Task<string> GetJwtTokenAsync(IEnumerable<Claim> claims);
    Task<string> GetJwtTokenAsync(IEnumerable<Claim> claims, CancellationToken cancellationToken);
    Task<bool> HaveClientClaimAsync(IEnumerable<Claim> accessClaims, Claim clientClaim);
    Task<bool> HaveClientClaimAsync(IEnumerable<Claim> accessClaims, Claim clientClaim, CancellationToken cancellationToken);
    Task<SecurityToken?> ValidateTokenAsync(string? token);
    Task<SecurityToken?> ValidateTokenAsync(string? token, CancellationToken cancellationToken);
}