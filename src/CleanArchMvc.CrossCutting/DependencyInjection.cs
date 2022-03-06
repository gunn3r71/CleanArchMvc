using CleanArchMvc.Application.Interfaces.Services;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Interfaces.Repositories;
using CleanArchMvc.Infrastructure.Contexts;
using CleanArchMvc.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using CleanArchMvc.Domain.Interfaces.Account;
using CleanArchMvc.Infrastructure.Identity;

namespace CleanArchMvc.CrossCutting
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");

            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString, x =>
                    {
                        x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        x.EnableRetryOnFailure(3);
                        x.CommandTimeout(10);
                        x.MigrationsHistoryTable("Migrations");
                    });
                });

            services.AddCustomAuthentication(configuration);

            services.ConfigureApplicationCookie(x => x.AccessDeniedPath = "/Account/Login");

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            services.AddAutoMapper(typeof(CategoryProfile));

            var handlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            services.AddMediatR(handlers);
        }
    }
}