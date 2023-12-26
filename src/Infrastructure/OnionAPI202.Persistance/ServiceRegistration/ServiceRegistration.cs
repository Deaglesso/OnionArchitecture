using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionAPI202.Application.Abstractions.Repositories;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Domain.Entities;
using OnionAPI202.Persistance.DAL;
using OnionAPI202.Persistance.Implementations.Repositories;
using OnionAPI202.Persistance.Implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default"),b=>b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;


                opt.User.RequireUniqueEmail = true;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();


        }
    }
}
