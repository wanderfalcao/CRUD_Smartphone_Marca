using CRUD_Smartphone_Marca.Data.Context;
using CRUD_Smartphone_Marca.Model.Interfaces.Repositories;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using CRUD_Smartphone_Marca.Service.Services;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.InversionOfControl
{
    public static class DependencyInjection
    {
        public static void RegisterInjections(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<DadosContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DadosContext")));

            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IMarcaService, MarcaService>();
            services.AddScoped<ISmartphoneRepository, SmartphoneRepository>();
            services.AddScoped<ISmartphoneSevice, SmartphoneService>();
        }
    }
}
