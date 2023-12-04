using BlogApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BlogApp.WebUI.Helpers;

namespace BlogApp.WebUI.Authorize
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ClaimRequirementFilter(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Rol yapısı ilerde devreye girebilir şimdilik dursun
            var role = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Role);
            if (role == null)
            {
                context.Result = new RedirectResult("/Auth/Login");
                return;
            }

            return;
        }
    }
}
