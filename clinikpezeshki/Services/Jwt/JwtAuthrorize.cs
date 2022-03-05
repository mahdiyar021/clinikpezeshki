using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

[AttributeUsage(AttributeTargets.Method)]
public class JwtAuthrorize : Attribute, IAuthorizationFilter
{

    private IEnumerable<Claim>? _AccessClaim, Clinetclaim;

    private static readonly JwtServices _JwtServices = new();


    public JwtAuthrorize(params JwtRoles[] Roles)
    {
        _AccessClaim = _JwtServices.GetAccessClaim(Roles);
    }


    public void OnAuthorization(AuthorizationFilterContext context)
    {

        context.HttpContext.Request.Cookies.TryGetValue(JwtConsts.CookieName, out string? TokenValue);

        SecurityToken? securityToken = _JwtServices.ValidateToken(TokenValue);

        if (securityToken == null)
        {
            context.HttpContext.Response.Redirect(JwtConsts.UnAuthorizePath+"?RedirectUrl="
                                                             + context.HttpContext.Request.Host.Value
                                                             + context.HttpContext.Request.Path);
            return;
        }

        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

        Clinetclaim = jwtSecurityToken.Claims.SkipLast(4);
        if (IsAuthenticate())
        {
            context.HttpContext.Response.Redirect(JwtConsts.UnAuthorizePath);
            return;
        }
        //for get claim in action(also add function getclaim in base controll)
        //    context.HttpContext.Items.Add(JwtConsts.HttpContextItemkey, Clinetclaim);

        //for refresh token
        //    context.HttpContext.Response.Cookies.Append(JwtConsts.CookieName,
         //                                              _JwtServices.GetJwtToken(Clinetclaim.SkipLast(4)),
         //                                              _JwtServices.GetCookieOptions());

    }


    private bool IsAuthenticate() => !_JwtServices.HaveClientClaim(_AccessClaim, Clinetclaim);

}

