using CRUD_Smartphone_Marca.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.Model.Interfaces.Services
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaEntity>> GetAllAsync();
        Task<MarcaEntity> GetByIdAsync(int id);
        Task InsertAsync(MarcaEntity updatedEntity);
        Task UpdateAsync(MarcaEntity insertedEntity);
        Task<bool> CheckNomeAsync(string nome, int id = -1);
        Task DeleteAsync(int id);
    }
}
