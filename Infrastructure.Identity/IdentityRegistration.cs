using Core.Application.Interfaces.Services;
using Infrastructure.Identity.Context;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity
{
    public static class IdentityRegistration
    {
        public static void AddIdentityLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("DatabaseInMomory"))
            {
                services.AddDbContext<IdentityContext>(option => option.UseInMemoryDatabase("InMemoryDB"));

            }
            else
            {
                services.AddDbContext<IdentityContext>(option =>
                    option.UseSqlServer(configuration.GetConnectionString("IdentityString"),
                        m=>m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }
            #endregion

            #region Identity Configurations
             services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(option =>
                 {
                     option.LoginPath = "/User";
                     option.AccessDeniedPath = "/Home";
                 }
             ) ;
            services.AddAuthentication();
            #endregion

            #region
            services.AddTransient<IAccountService,AccountService>();
            
            #endregion
        }
    }
}
