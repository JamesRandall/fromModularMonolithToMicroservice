using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DecomposedApplication.Filters
{
    public class SetClaimsForDemoFilter : IResourceFilter
    {
        private const string UserId = "9ADBC508-DC92-4B0E-AA24-DC48EB92021D";

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.User.AddIdentity(new ClaimsIdentity(new[]
                {
                    new Claim("UserId", UserId)
                })
            );
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }
    }

}
