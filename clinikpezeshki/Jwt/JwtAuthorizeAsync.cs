#nullable disable
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

[AttributeUsage(AttributeTargets.Method)]
public class JwtAuthrorizeAsync : Attribute, IAsyncAuthorizationFilter
{

    private readonly IEnumerable<Claim> _AccessClaim;

    private readonly IJwtServices _JwtServices;


    public JwtAuthrorizeAsync(params JwtRoles[] Roles)
    {
        _JwtServices = new JwtServices();

        _AccessClaim = _JwtServices.GetAccessClaim(Roles);
    }


    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        SecurityToken? securityToken;

        JwtSecurityToken jwtSecurityToken;


        if (!context.HttpContext.Request.Cookies.TryGetValue(JwtOptions.CookieName, out string? TokenValue))
        {
            RedirectToLoginPage();
            return;
        }

        securityToken = await _JwtServices.ValidateTokenAsync(TokenValue, context.HttpContext.RequestAborted);


        if (securityToken == null)
        {
            RedirectToLoginPage();
            return;
        }


        jwtSecurityToken = securityToken as JwtSecurityToken;


        if (await IsNotAuthenticate(jwtSecurityToken.Claims.First(), context.HttpContext.RequestAborted))
        {
            RedirectToLoginPage();
            return;
        }


        //for get claim in action(also add function getclaim in base controll)
        //  context.HttpContext.Items.Add(JwtOptions.HttpContextItemkey,jwtSecurityToken.Claims.SkipLast(4));


        //for refresh token
        //  context.HttpContext.Response.Cookies.Append(JwtConsts.CookieName,
        //                      await _JwtServices.GetJwtTokenAsync(jwtSecurityToken.Claims.SkipLast(4),context.HttpContext.RequestAborted),
        //                      await _JwtServices.GetCookieOptionsAsync(context.HttpContext.RequestAborted));

        void RedirectToLoginPage()
        {
            context.HttpContext.Response.Redirect(JwtOptions.UnAuthorizePath + JwtOptions.QueryStringKey
                                                             + context.HttpContext.Request.Host.Value
                                                             + context.HttpContext.Request.Path);
        }
    }


    private async Task<bool> IsNotAuthenticate(Claim clientClaim,
                                                CancellationToken cancellationToken)

     => !await _JwtServices.HaveClientClaimAsync(_AccessClaim, clientClaim, cancellationToken);

}




