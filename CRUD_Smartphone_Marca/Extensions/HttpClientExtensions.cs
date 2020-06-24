using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using CRUD_Smartphone_Marca.Model.Options;
using CRUD_Smartphone_Marca.MVC.HttpServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD_Smartphone_Marca.MVC.Extensions
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var dadosHttpOptionsSection = configuration.GetSection(nameof(DadosHttpOptions));
            var dadosHttpOptions = dadosHttpOptionsSection.Get<DadosHttpOptions>();

            services.AddHttpClient(dadosHttpOptions.Name, x => { x.BaseAddress = dadosHttpOptions.ApiBaseUrl; });

            services.AddScoped<ISmartphoneSevice, SmartphoneHttpService>();
            services.AddScoped<IMarcaHttpService, MarcaHttpService>();            
        }
    }
}
