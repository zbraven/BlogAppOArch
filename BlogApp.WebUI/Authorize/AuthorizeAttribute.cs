using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Authorize
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute() 
            : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { };
        }
    }
}
