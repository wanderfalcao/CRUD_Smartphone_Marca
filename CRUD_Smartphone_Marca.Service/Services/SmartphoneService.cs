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

        public SmartphoneService(
            ISmartphoneRepository marcaRepository)
        {
            _smartphoneRepository = marcaRepository;
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

        public async Task InsertAsync(SmartphoneEntity insertedEntity)
        {
            await _smartphoneRepository.InsertAsync(insertedEntity);
        }

        public async Task UpdateAsync(SmartphoneEntity updatedEntity)
        {
            await _smartphoneRepository.UpdateAsync(updatedEntity);
        }
    }
}
