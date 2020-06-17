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
    public class SmartphoneViewModel
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

        public List<SelectListItem> Marcas { get; }

        public SmartphoneViewModel() { }

        public SmartphoneViewModel(SmartphoneEntity smartphoneModel)
        {
            Nome = smartphoneModel.Nome;
            Modelo = smartphoneModel.Modelo;
            Lancamento = smartphoneModel.Lancamento;
            Valor = smartphoneModel.Valor;
            MarcaEntityId = smartphoneModel.MarcaEntityId;
            Marca = smartphoneModel.Marca;
        }

        public SmartphoneViewModel(SmartphoneEntity smartphoneModel, IEnumerable<MarcaEntity> marcas) : this(smartphoneModel)
        {
            Marcas = ToMarcasSelectListItem(marcas);
        }

        public SmartphoneViewModel(IEnumerable<MarcaEntity> marcas)
        {
            Marcas = ToMarcasSelectListItem(marcas);
        }

        private static List<SelectListItem> ToMarcasSelectListItem(IEnumerable<MarcaEntity> marcas)
        {
            return marcas.Select(x => new SelectListItem
            { Text = $"{x.Nome} {x.Pais}", Value = x.Id.ToString() }).ToList();
        }

    }
}
