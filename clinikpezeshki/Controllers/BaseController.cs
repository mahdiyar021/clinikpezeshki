
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Web;

namespace clinikpezeshki.Controllers
{
    public class BaseController : Controller
    {

        protected void AppendCookie(string key, string value)

         => HttpContext.Response.Cookies.Append(key, value);


        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.QueryString.HasValue)
            {
                string responseString = context.HttpContext.Request.QueryString.Value;
                var dict = HttpUtility.ParseQueryString(responseString);
                string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
                MessageboxVm? respObj = JsonConvert.DeserializeObject<MessageboxVm>(json);

                if (respObj?.IsDanger != null && respObj?.MsgTxt != null)
                {
                    ViewData.Add(nameof(MessageboxVm), respObj);
                }

            }

            return base.OnActionExecutionAsync(context, next);
        }
        protected void AppendCookie(string key, string value, CookieOptions options)

         => HttpContext.Response.Cookies.Append(key, value, options);

        protected void MessageBoxShow(bool isDanger, string msgTxt)
        {
            ViewData[nameof(MessageboxVm)] = new MessageboxVm(isDanger, msgTxt);
        }

        protected string? GetCookieValue(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string? value);

            return value;
        }

        protected bool IsNull(object? obj)
        {
            if (obj == null)
            {
                return true;
            }
            return false;
        }
    }
}
