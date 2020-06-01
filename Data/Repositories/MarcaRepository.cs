using CRUD_Smartphone_Marca.Data.Context;
using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly DadosContext _context;

        public MarcaRepository(
            DadosContext dadosContext)
        {
            _context = dadosContext;
        }

        public async Task<bool> CheckNomeAsync(string nome, int id)
        {
            var nomeExists = await _context.MarcaModel.AnyAsync(x => x.Nome == nome && x.Id != id);
            return nomeExists;
        }

        public async Task DeleteAsync(int id)
        {
            var marcaModel = await _context.MarcaModel.FindAsync(id);
            _context.MarcaModel.Remove(marcaModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MarcaEntity>> GetAllAsync()
        {
            return await _context.MarcaModel.ToListAsync();
        }

        public async Task<MarcaEntity> GetByIdAsync(int id)
        {
            return await _context.MarcaModel.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(MarcaEntity updatedEntity)
        {
            _context.Add(updatedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MarcaEntity insertedEntity)
        {
            _context.Update(insertedEntity);
            await _context.SaveChangesAsync();
        }
    }
}
