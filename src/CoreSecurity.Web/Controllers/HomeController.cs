using CoreSecurity.Web.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreSecurity.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        public HomeController(UserManager<ApplicationIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            ApplicationIdentityUser user = _userManager.GetUserAsync(HttpContext.User).Result;

            if (user != null)
            {
                ViewBag.Message = $"Welcome {user.FirstName}!";

                if (_userManager.IsInRoleAsync(user, "User").Result)
                {
                    ViewBag.RoleMessage = "You are a user.";
                }

            }
            return View();
        }
    }
}
