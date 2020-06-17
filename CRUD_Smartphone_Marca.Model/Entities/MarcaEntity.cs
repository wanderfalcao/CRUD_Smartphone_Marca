using CRUD_Smartphone_Marca.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.Domain.Models
{
    public class MarcaEntity
    {
        //[Column("MarcaPk")]
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        [Remote(
            action: "CheckNome",
            controller: "Marca",
            AdditionalFields = nameof(Id))]
        public String Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public String Pais { get; set; }
        public int qtdSmartphone { get; set; }

        public List<SmartphoneEntity> Smartphone { get; set; }

    }
}
