using CRUD_Smartphone_Marca.Model.Entities;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using CRUD_Smartphone_Marca.Model.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.MVC.HttpServices
{
    public class SmartphoneHttpService : ISmartphoneSevice
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<DadosHttpOptions> _dadosHttpOptions;
        private readonly SignInManager<IdentityUser> _signInManager;

        public SmartphoneHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<DadosHttpOptions> dadosHttpOptions,
            SignInManager<IdentityUser> signInManager)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _dadosHttpOptions = dadosHttpOptions ?? throw new ArgumentNullException(nameof(dadosHttpOptions));
            _signInManager = signInManager;

            _httpClient = httpClientFactory.CreateClient(dadosHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_dadosHttpOptions.CurrentValue.Timeout);
        }

        public async Task<IEnumerable<SmartphoneEntity>> GetAllAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync(_dadosHttpOptions.CurrentValue.SmartphonePath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<SmartphoneEntity>>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
            }

            return null;
        }

        public async Task<SmartphoneEntity> GetByIdAsync(int id)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.SmartphonePath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<SmartphoneEntity>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
                new RedirectToActionResult("Smartphone", "Index", null);
            }

            return null;
        }

        public async Task InsertAsync(SmartphoneMarcaAggregateEntity smartphoneMarcaAggregateEntity)
        {
            var uriPath = $"{_dadosHttpOptions.CurrentValue.SmartphonePath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(smartphoneMarcaAggregateEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(SmartphoneEntity updatedEntity)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.SmartphonePath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }

        }

        public async Task DeleteAsync(int id)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.SmartphonePath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }
    }
}
