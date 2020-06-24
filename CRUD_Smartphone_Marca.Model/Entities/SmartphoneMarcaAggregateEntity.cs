using CRUD_Smartphone_Marca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Smartphone_Marca.Model.Entities
{
    public class SmartphoneMarcaAggregateEntity
    {
        public MarcaEntity MarcaEntity { get; set; }
        public SmartphoneEntity SmartphoneEntity { get; set; }
    }
}
