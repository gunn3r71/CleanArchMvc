using System.Threading.Tasks;
using CleanArchMvc.Domain.Interfaces.Account;
using CleanArchMvc.WebUI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticationService;

        public AccountController(IAuthenticate authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var model = new LoginViewModel() {ReturnUrl = returnUrl};

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authenticationService.AuthenticateAsync(model.Email, model.Password);

            if (result)
            {
                if (!string.IsNullOrWhiteSpace(model.ReturnUrl)) LocalRedirect(model.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _authenticationService.RegisterUserAsync(model.Email, model.Password);

            if (result) return Redirect("/");

            ModelState.AddModelError(string.Empty, "Invalid register attempt.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
