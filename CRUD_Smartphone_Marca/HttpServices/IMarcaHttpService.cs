using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.MVC.HttpServices
{
    public interface IMarcaHttpService : IMarcaService
    {
        Task<HttpResponseMessage> GetByIdHttpAsync(int id);
    }
}
