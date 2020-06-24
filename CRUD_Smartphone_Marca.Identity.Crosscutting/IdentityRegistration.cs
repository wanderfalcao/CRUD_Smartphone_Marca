using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.Identity.Crosscutting
{
    public static class IdentityRegistration
    {
        public static void RegisterIdentityForMvc(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddDbContext(services, configuration);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<LoginContext>();
        }

        public static void RegisterIdentityForWebApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddDbContext(services, configuration);

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<LoginContext>();
        }
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LoginContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LoginContextConnection")));
        }
    }
}
