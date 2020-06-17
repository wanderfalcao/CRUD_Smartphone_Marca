using CRUD_Smartphone_Marca.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUD_Smartphone_Marca.Model.Entities
{
    public class SmartphoneEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Modelo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }
        public float Valor { get; set; }

        public int? MarcaEntityId { get; set; }
        public MarcaEntity Marca { get; set; }
    }
}
