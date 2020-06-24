using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CRUD_Smartphone_Marca.MVC.HttpServices
{
    public class MarcaHttpService : IMarcaHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<DadosHttpOptions> _dadosHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MarcaHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<DadosHttpOptions> dadosHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _dadosHttpOptions = dadosHttpOptions ?? throw new ArgumentNullException(nameof(dadosHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;
            ;

            _httpClient = httpClientFactory?.CreateClient(dadosHttpOptions.CurrentValue.Name) ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient.Timeout = TimeSpan.FromMinutes(_dadosHttpOptions.CurrentValue.Timeout);
        }

        public async Task<IEnumerable<MarcaEntity>> GetAllAsync()
        {
            try
            {
                var marcas = await _httpClient.GetFromJsonAsync<List<MarcaEntity>>(_dadosHttpOptions.CurrentValue.MarcaPath);
                return marcas;
            }
            catch (HttpRequestException e) when (e.Message.Contains("401"))
            {
                await _signInManager.SignOutAsync();
                return null;
            
            }
            
            //var httpResponseMessage = await _httpClient.GetAsync(_dadosHttpOptions.CurrentValue.MarcaPath);

            //if (!httpResponseMessage.IsSuccessStatusCode)
            //{
            //    await _signInManager.SignOutAsync();
            //    return null;
            //}

            //return JsonConvert.DeserializeObject<List<MarcaEntity>>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<MarcaEntity> GetByIdAsync(int id)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.MarcaPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);
            return JsonConvert.DeserializeObject<MarcaEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> GetByIdHttpAsync(int id)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.MarcaPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);


            return httpResponseMessage;
        }

        public async Task InsertAsync(MarcaEntity insertedEntity)
        {
            var uriPath = $"{_dadosHttpOptions.CurrentValue.MarcaPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(insertedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(MarcaEntity updatedEntity)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.MarcaPath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.MarcaPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task<bool> CheckNomeAsync(string nome, int id = -1)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.MarcaPath}/CheckNome/{nome}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return bool.Parse(result);
        }
    }
}
