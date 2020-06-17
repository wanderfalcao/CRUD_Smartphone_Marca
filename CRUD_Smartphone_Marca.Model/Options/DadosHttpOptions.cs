using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.Model.Options
{
    public class DadosHttpOptions
    {
        public Uri ApiBaseUrl { get; set; }
        public string MarcaPath { get; set; }
        public string SmartphonePath { get; set; }
        public string Name { get; set; }
        public int Timeout { get; set; }
    }
}
