﻿using System.Threading.Tasks;
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

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, true);

            return result.Succeeded;
        }

        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(applicationUser);

            if (result.Succeeded) await AuthenticateAsync(email, password);

            return result.Succeeded;
        }

        public async Task LogoutAsync() =>
            await _signInManager.SignOutAsync();
    }
}