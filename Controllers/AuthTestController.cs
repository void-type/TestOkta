using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OktaTest.Controllers
{
    // TODO: POC for AuthTest. Please remove when you have a React login page.
    [Route("authtest")]
    public class AuthTestController : Controller
    {
        // Test login page (Okta sample embedded widget)
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("echo")]
        public ActionResult<string> Echo(string input)
        {
            return Ok(input);
        }

        [Authorize]
        [HttpGet]
        [Route("whoami")]
        public Dictionary<string, string> GetAuthorized()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity principal)
            {
                return new();
            }

            return principal.Claims
               .GroupBy(claim => claim.Type)
               .ToDictionary(claim => claim.Key, claim => claim.First().Value);
        }
    }
}
