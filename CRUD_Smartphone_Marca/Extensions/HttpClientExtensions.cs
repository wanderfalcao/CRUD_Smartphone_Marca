using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using CRUD_Smartphone_Marca.Model.Options;
using CRUD_Smartphone_Marca.MVC.HttpServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.MVC.Extensions
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var bibliotecaHttpOptionsSection = configuration.GetSection(nameof(DadosHttpOptions));
            var bibliotecaHttpOptions = bibliotecaHttpOptionsSection.Get<DadosHttpOptions>();

            services.AddHttpClient(bibliotecaHttpOptions.Name, x => { x.BaseAddress = bibliotecaHttpOptions.ApiBaseUrl; });

            services.AddScoped<IMarcaService, MarcaHttpService>();
            services.AddScoped<ISmartphoneSevice, SmartphoneHttpService>();
        }
    }
}
