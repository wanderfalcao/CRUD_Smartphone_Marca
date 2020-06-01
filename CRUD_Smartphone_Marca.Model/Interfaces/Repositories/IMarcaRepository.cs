using CRUD_Smartphone_Marca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.Model.Interfaces.Repositories
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<MarcaEntity>> GetAllAsync();
        Task<MarcaEntity> GetByIdAsync(int id);
        Task InsertAsync(MarcaEntity updatedEntity);
        Task UpdateAsync(MarcaEntity insertedEntity);
        Task DeleteAsync(int id);
        Task<bool> CheckNomeAsync(string nome, int id);
    }
}
