using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public sealed class JwtServices : IJwtServices
{
    private readonly JwtSecurityTokenHandler _JwtSecurityTokenHandler;

    private readonly SigningCredentials _SigningCredentials;

    private readonly TokenValidationParameters _TokenValidationParameters = new()
    {
        IssuerSigningKey = GetSymmetricSecurityKey(),

        ValidAudience = AppSetting.JwtAudience,

        ValidIssuer = AppSetting.JwtIssuer,

    };

    public JwtServices()
    {

        _SigningCredentials = new(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

        _JwtSecurityTokenHandler = new();

    }

    public SecurityToken? ValidateToken(string? token)
    {

        try
        {
            _JwtSecurityTokenHandler.ValidateToken(token, _TokenValidationParameters, out SecurityToken securityToken);
            
            return securityToken;
        }
        catch (Exception)
        {
            return null;
        }

    }

    public CookieOptions GetCookieOptions()
    {
        return new CookieOptions()
        {
            Expires = DateTime.UtcNow.AddDays(JwtConsts.ExpireDay),
            Secure = true,
            SameSite = SameSiteMode.None
        };
    }

    public IEnumerable<Claim> GetAccessClaim(JwtRoles[] roles)
    {
        List<Claim> claims = new();

        foreach (var role in roles)
        {
            Claim c = role switch
            {
                JwtRoles.doctor => new Claim(JwtClaimTypes.Role, JwtClaimRoleValue.doctor),

                JwtRoles.employee => new Claim(JwtClaimTypes.Role, JwtClaimRoleValue.employee)

            };

            claims.Add(c);
        }
        return claims;
    }

    public bool HaveClientClaim(IEnumerable<Claim>? accessClaim, IEnumerable<Claim>? clientClaim)
    {

        if(accessClaim == null || clientClaim == null) return false;

        foreach (var AccessClaimRole in accessClaim)
        {
            foreach (var clientClaimRole in clientClaim)
            {
                if (AccessClaimRole.Type == clientClaimRole.Type &&
                    AccessClaimRole.Value == clientClaimRole.Value)
                {
                    return true;
                }

            }
        }

        return false;
    }

    private static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        byte[] SecretKeyByte = Encoding.UTF8.GetBytes(AppSetting.JwtSecretKey);

        return new SymmetricSecurityKey(SecretKeyByte);
    }

    public string GetJwtToken(IEnumerable<Claim> claims)
    {

        JwtSecurityToken token = new(AppSetting.JwtIssuer,
                                     AppSetting.JwtAudience,
                                     claims,
                                     DateTime.Now,
                                     DateTime.Now.AddDays(JwtConsts.ExpireDay),
                                     _SigningCredentials);

        return _JwtSecurityTokenHandler.WriteToken(token);
    }
    public async Task<string> GetJwtTokenAsync(IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        await Task.FromCanceled(cancellationToken);

        JwtSecurityToken token = new(AppSetting.JwtIssuer,
                         AppSetting.JwtAudience,
                         claims,
                         DateTime.UtcNow,
                         DateTime.UtcNow.AddDays(JwtConsts.ExpireDay),
                         _SigningCredentials);

        await Task.FromCanceled(cancellationToken);

        return _JwtSecurityTokenHandler.WriteToken(token);

    }

   public IEnumerable<Claim>? GetClaims(string? securityToken)
    {
        SecurityToken? secToken = ValidateToken(securityToken);

        if (secToken == null) return null;
         
        var jct = secToken as JwtSecurityToken;

        return jct?.Claims;

    }
}

