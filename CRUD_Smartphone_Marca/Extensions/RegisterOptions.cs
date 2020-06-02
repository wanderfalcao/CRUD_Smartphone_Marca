using CRUD_Smartphone_Marca.Model.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.MVC.Extensions
{
    public static class RegisterOptions
    {
        public static void RegisterConfigurations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<TestOption>(configuration.GetSection("TestOption"));
        }
    }
}
