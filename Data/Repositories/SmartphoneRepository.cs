using CRUD_Smartphone_Marca.Data.Context;
using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Entities;
using CRUD_Smartphone_Marca.Model.Exceptions;
using CRUD_Smartphone_Marca.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SmartphoneRepository : ISmartphoneRepository
    {
        private readonly DadosContext _context;

        public SmartphoneRepository(
            DadosContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var smartphoneModel = await _context.SmartphoneModel.FindAsync(id);
            _context.SmartphoneModel.Remove(smartphoneModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SmartphoneEntity>> GetAllAsync()
        {
            return await _context.SmartphoneModel.ToListAsync();
        }

        public async Task<SmartphoneEntity> GetByIdAsync(int id)
        {
            return await _context.SmartphoneModel.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(SmartphoneEntity insertedEntity)
        {
            _context.Add(insertedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SmartphoneEntity updatedEntity)
        {
            try
            {
                _context.Update(updatedEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetByIdAsync(updatedEntity.Id) == null)
                {
                    throw new RepositoryException("Smartphone não encontrado!");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
