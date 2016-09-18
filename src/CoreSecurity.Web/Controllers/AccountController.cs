using CoreSecurity.Web.Domain;
using CoreSecurity.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreSecurity.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _loginManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationIdentityUser> userManager,
                                 SignInManager<ApplicationIdentityUser> loginManager,
                                 RoleManager<ApplicationIdentityRole> roleManager)
        {
            _userManager = userManager;
            _loginManager = loginManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationIdentityUser user = new ApplicationIdentityUser();
                user.UserName = registerViewModel.UserName;
                user.Email = registerViewModel.Email;
                user.FirstName = registerViewModel.FirstName;
                user.LastName = registerViewModel.LastName;
                user.BirthDate = registerViewModel.BirthDate;

                IdentityResult result = _userManager.CreateAsync(user, registerViewModel.Password).Result;

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("User").Result)
                    {
                        ApplicationIdentityRole role = new ApplicationIdentityRole();
                        role.Name = "User";
                        role.Description = "Can access user actions.";
                        IdentityResult roleResult = _roleManager.
                        CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Error while creating role!");
                            return View(registerViewModel);
                        }
                    }

                    _userManager.AddToRoleAsync(user, "User").Wait();

                    return RedirectToAction("Login", "Account");
                }
            }
            return View(registerViewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var result = _loginManager.PasswordSignInAsync(obj.UserName, obj.Password, obj.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login!");
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            _loginManager.SignOutAsync().Wait();

            return RedirectToAction("Login", "Account");
        }
    }
}
