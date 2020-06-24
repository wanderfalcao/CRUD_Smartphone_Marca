using CRUD_Smartphone_Marca.Model.Entities;
using CRUD_Smartphone_Marca.Model.Exceptions;
using CRUD_Smartphone_Marca.Model.Interfaces.Repositories;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.Service.Services
{
    public class SmartphoneService : ISmartphoneSevice
    {
        private readonly ISmartphoneRepository _smartphoneRepository;
        private readonly IMarcaService _marcaService;

        public SmartphoneService(
            ISmartphoneRepository smartphoneRepository,
            IMarcaService marcaService)
        {
            _smartphoneRepository = smartphoneRepository;
            _marcaService = marcaService;
        }

        public async Task DeleteAsync(int id)
        {
            await _smartphoneRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SmartphoneEntity>> GetAllAsync()
        {
            return await _smartphoneRepository.GetAllAsync();
        }

        public async Task<SmartphoneEntity> GetByIdAsync(int id)
        {
            return await _smartphoneRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(SmartphoneMarcaAggregateEntity smartphoneMarcaAggregateEntity)
        {
            if (!(smartphoneMarcaAggregateEntity.MarcaEntity is null) &&
                !string.IsNullOrWhiteSpace(smartphoneMarcaAggregateEntity.MarcaEntity.Nome) &&
                !string.IsNullOrWhiteSpace(smartphoneMarcaAggregateEntity.MarcaEntity.Pais))
            {
                await _marcaService.InsertAsync(smartphoneMarcaAggregateEntity.MarcaEntity);
            }

            smartphoneMarcaAggregateEntity.SmartphoneEntity.Marca = smartphoneMarcaAggregateEntity.MarcaEntity;
            await _smartphoneRepository.InsertAsync(smartphoneMarcaAggregateEntity.SmartphoneEntity);
        }

        public async Task UpdateAsync(SmartphoneEntity updatedEntity)
        {
            await _smartphoneRepository.UpdateAsync(updatedEntity);
        }
    }
}
