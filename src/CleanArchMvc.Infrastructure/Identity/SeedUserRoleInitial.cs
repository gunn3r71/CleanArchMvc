using System.Collections.Generic;
using CleanArchMvc.Domain.Interfaces.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infrastructure.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, 
                                   RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            if (_userManager.FindByNameAsync("user@admin").Result is not null) return;
            
            var user = new ApplicationUser
            {
                Email = "user@admin.com",
                UserName = "user@admin",
                EmailConfirmed = true,
                LockoutEnabled = true
            };

           var result =  _userManager.CreateAsync(user, "admin1A@").Result;

           if (result.Succeeded) _userManager.AddToRoleAsync(user, "Admin");
        }

        public void SeedRoles()
        {
            var initialRoles = new List<IdentityRole>
            {
                new IdentityRole("Admin"),
                new IdentityRole("User")
            };

            initialRoles.ForEach(x =>
            {
                if (!_roleManager.RoleExistsAsync(x.Name).Result) _roleManager.CreateAsync(x);
            });
        }
    }
}