using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.Model.Interfaces.Repositories
{
    public interface ISmartphoneRepository
    {
        Task<IEnumerable<SmartphoneEntity>> GetAllAsync();
        Task<SmartphoneEntity> GetByIdAsync(int id);
        Task InsertAsync(SmartphoneEntity insertedEntity);
        Task UpdateAsync(SmartphoneEntity updatedEntity);
        Task DeleteAsync(int id);
    }
}
