using CleanArchMvc.Application.Interfaces.Services;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Interfaces.Repositories;
using CleanArchMvc.Infrastructure.Contexts;
using CleanArchMvc.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(typeof(CategoryProfile));

            return services;
        }
    }
}