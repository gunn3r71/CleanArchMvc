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
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomDatabase(configuration);

            services.AddCustomAuthentication(configuration);

            services.ConfigureApplicationCookie(x => x.AccessDeniedPath = "/Account/Login");

            services.RegisterServices();

            services.AddAutoMapper(typeof(CategoryProfile));

            var handlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            services.AddMediatR(handlers);

            return services;
        }

        public static IServiceCollection AddApiInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomDatabase(configuration);

            services.AddCustomAuthentication(configuration);

            services.AddCustomJwt(configuration);

            services.AddAutoMapper(typeof(CategoryProfile));

            services.RegisterServices();

            var handlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            services.AddMediatR(handlers);

            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        }

        private static void AddCustomDatabase(this IServiceCollection services, IConfiguration configuration)
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
        }
    }
}