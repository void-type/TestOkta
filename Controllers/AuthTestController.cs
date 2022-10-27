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
        [HttpGet]
        [Route("whoami")]
        public UserInfo GetAuthorized()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity principal)
            {
                throw new Exception("Identity is not found.");
            }

            var userName = principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var claims = principal.Claims
               .GroupBy(claim => claim.Type)
               .ToDictionary(group => group.Key, group => string.Join(",", group.Select(x => x.Value)));

            var groups = principal.Claims.Where(c => c.Type == "groups").Select(c => c.Value) ?? Enumerable.Empty<string>();

            return new UserInfo(
                userName,
                groups
            );
        }

        [Authorize("PrivilegedOnly")]
        [Route("echo")]
        public ActionResult<string> Echo(string input)
        {
            return Ok(input);
        }
    }

    public record UserInfo(string? UserName, IEnumerable<string> Groups);
}
