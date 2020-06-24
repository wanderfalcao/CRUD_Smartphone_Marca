using CRUD_Smartphone_Marca.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.Model.Interfaces.Services
{
    public interface ISmartphoneSevice
    {
        Task<IEnumerable<SmartphoneEntity>> GetAllAsync();
        Task<SmartphoneEntity> GetByIdAsync(int id);
        Task InsertAsync(SmartphoneMarcaAggregateEntity smartphoneMarcaAggregateEntity);
        Task UpdateAsync(SmartphoneEntity updatedEntity);
        Task DeleteAsync(int id);
    }
}
