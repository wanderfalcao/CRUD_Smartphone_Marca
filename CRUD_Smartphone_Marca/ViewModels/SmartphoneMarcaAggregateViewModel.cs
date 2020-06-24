using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.MVC.ViewModels
{
    public class SmartphoneMarcaAggregateViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string NomeSmartphone { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Modelo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }
        public float Valor { get; set; }

        public int? MarcaEntityId { get; set; }
        public MarcaEntity Marca { get; set; }

        public List<SelectListItem>? Marcas { get; }

        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string NomeMarca { get; set; }

        public String Pais { get; set; }

        public int qtdSmartphone { get; set; }

        public SmartphoneMarcaAggregateViewModel(){}

        public SmartphoneMarcaAggregateViewModel(IEnumerable<MarcaEntity>marcas)
        {
            Marcas = ToMarcaSelectListItem(marcas);
        }

        private static List<SelectListItem> ToMarcaSelectListItem(IEnumerable<MarcaEntity> marcas)
        {
            return marcas.Select(x => new SelectListItem
            { Text = $"{x.Nome} {x.Pais}", Value = x.Id.ToString() }).ToList();
        }
        public SmartphoneMarcaAggregateEntity ToAggregateEntity()
        {
            var aggregateEntity = new SmartphoneMarcaAggregateEntity
            {
                SmartphoneEntity = new SmartphoneEntity
                {
                    Nome = NomeSmartphone,
                    Modelo = Modelo,
                    Lancamento = Lancamento,
                    Valor = Valor,
                    MarcaEntityId = MarcaEntityId ?? 0
                }
            };

            if (!string.IsNullOrWhiteSpace(Modelo) ||
                string.IsNullOrWhiteSpace(NomeMarca))
                return aggregateEntity;

            aggregateEntity.MarcaEntity = new MarcaEntity
            {
                Nome = NomeMarca,
                Pais = Pais,
                qtdSmartphone = qtdSmartphone,
                Id = MarcaEntityId ?? 0
            };

            return aggregateEntity;
        }
    }
}
