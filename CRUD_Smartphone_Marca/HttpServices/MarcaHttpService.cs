﻿using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using CRUD_Smartphone_Marca.Model.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.MVC.HttpServices
{
    public class MarcaHttpService : IMarcaService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<DadosHttpOptions> _dadosHttpOptions;

        public MarcaHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<DadosHttpOptions> dadosHttpOptions)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _dadosHttpOptions = dadosHttpOptions ?? throw new ArgumentNullException(nameof(DadosHttpOptions));

            _httpClient = httpClientFactory.CreateClient(dadosHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_dadosHttpOptions.CurrentValue.Timeout);
        }

        public async Task<IEnumerable<MarcaEntity>> GetAllAsync()
        {
            var result = await _httpClient.GetStringAsync(_dadosHttpOptions.CurrentValue.MarcaPath);
            return JsonConvert.DeserializeObject<List<MarcaEntity>>(result);
        }

        public async Task<MarcaEntity> GetByIdAsync(int id)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.MarcaPath}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<MarcaEntity>(result);
        }

        public async Task InsertAsync(MarcaEntity insertedEntity)
        {
            var uriPath = $"{_dadosHttpOptions.CurrentValue.MarcaPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(insertedEntity), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(uriPath, httpContent);
        }

        public async Task UpdateAsync(MarcaEntity updatedEntity)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.MarcaPath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(pathWithId, httpContent);
        }

        public async Task DeleteAsync(int id)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.MarcaPath}/{id}";
            await _httpClient.DeleteAsync(pathWithId);
        }

        public async Task<bool> CheckNomeAsync(string nome, int id = -1)
        {
            var pathWithId = $"{_dadosHttpOptions.CurrentValue.MarcaPath}/CheckNome/{nome}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return bool.Parse(result);
        }
    }
}
