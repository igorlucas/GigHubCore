using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GigHub.Tests.Extensions
{
    public static class ApiControllerExtensions
    {
        public static void MockCurrentUser(this Controller controller, string userId, string username)
        {            
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username),
                                      new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId)}
                    , "test"))
                }
            };           
        }        
    }
}
