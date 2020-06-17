using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.MVC.HttpServices
{
    public interface IMarcaHttpService
    {
        Task<HttpResponseMessage> GetByIdHttpAsync(int id);
    }
}
