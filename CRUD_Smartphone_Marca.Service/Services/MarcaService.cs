using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Exceptions;
using CRUD_Smartphone_Marca.Model.Interfaces.Repositories;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.Service.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcaService(
            IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<bool> CheckNomeAsync(string nome, int id = -1)
        {
            return await _marcaRepository.CheckNomeAsync(nome, id);
        }

        public async Task DeleteAsync(int id)
        {
            await _marcaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MarcaEntity>> GetAllAsync()
        {
            return await _marcaRepository.GetAllAsync();
        }

        public async Task<MarcaEntity> GetByIdAsync(int id)
        {
            return await _marcaRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(MarcaEntity insertedEntity)
        {
            var nomeExists = await _marcaRepository.CheckNomeAsync(insertedEntity.Nome, insertedEntity.Id);
            if (nomeExists)
            {
                throw new EntityValidationException(nameof(MarcaEntity.Nome), $"Nome: {insertedEntity.Nome} já existe!");
            }
            await _marcaRepository.InsertAsync(insertedEntity);
        }

        public async Task UpdateAsync(MarcaEntity updatedEntity)
        {
            var nomeExists = await _marcaRepository.CheckNomeAsync(updatedEntity.Nome, updatedEntity.Id);
            if (nomeExists)
            {
                throw new EntityValidationException(nameof(MarcaEntity.Nome), $"Nome: {updatedEntity.Nome} já existe!");
            }
            await _marcaRepository.UpdateAsync(updatedEntity);
        }
    }
}
