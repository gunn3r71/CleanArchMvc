using System.Threading.Tasks;
using CleanArchMvc.Domain.Interfaces.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infrastructure.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(UserManager<ApplicationUser> userManager,
                                   SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var user = await _signInManager.PasswordSignInAsync(email, password, false, true);

            return user.Succeeded;
        }

        public async Task<bool> RegisterUserTask(string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(applicationUser);

            if (result.Succeeded) await Authenticate(email, password);

            return result.Succeeded;
        }

        public async Task Logout() =>
            await _signInManager.SignOutAsync();
    }
}