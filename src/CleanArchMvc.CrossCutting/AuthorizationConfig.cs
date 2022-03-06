using CleanArchMvc.Infrastructure.Contexts;
using CleanArchMvc.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.CrossCutting
{
    internal static class AuthorizationConfig
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");

            services.AddDbContext<AuthenticationDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString, x =>
                    {
                        x.MigrationsAssembly(typeof(AuthenticationDbContext).Assembly.FullName);
                        x.EnableRetryOnFailure(3);
                        x.CommandTimeout(10);
                        x.MigrationsHistoryTable("Migrations");
                    });
                });

            services.AddIdentity<ApplicationUser, IdentityRole>(ConfigureIdentity)
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void ConfigureIdentity(IdentityOptions options)
        {
            options.User = new UserOptions
            {
                RequireUniqueEmail = true
            };

            options.Password = new PasswordOptions
            {
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonAlphanumeric = true
            };
        }
    }
}
