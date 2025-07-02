using EasyTime.Application.Contract.IServices;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyTime.Application.Attributes
{
    public class RolemanagerAttribute : ActionFilterAttribute
    {
        private readonly string role;

        public RolemanagerAttribute(string role)
        {
            this.role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool autorize = false;
            string headerValue = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(headerValue))
            {
                if (headerValue.StartsWith("Bearer "))
                {
                    var service = (ITokenGenerator)context.HttpContext.RequestServices.GetService(typeof(ITokenGenerator));
                    var token = headerValue.Replace("Bearer ", "");
                    var claimPrincipal = service.ValidateToken(token);
                    var claims = claimPrincipal.Claims.Where(c => c.Type == "Role" && c.Value == role);
                    if (claimPrincipal != null)
                    {
                        var userId = claimPrincipal.Claims.FirstOrDefault(c => c.Type == "userid").Value;
                        var userContext = (IUserContext)context.HttpContext.RequestServices.GetService(typeof(IUserContext));
                    }
                    autorize = true;
                    return;
                }
            }
            if (autorize == false)
            {
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
