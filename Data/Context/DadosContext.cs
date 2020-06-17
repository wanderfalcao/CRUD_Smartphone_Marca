using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Entities;
using Data.Context;
using Microsoft.EntityFrameworkCore;


namespace CRUD_Smartphone_Marca.Data.Context
{
    public class DadosContext : DbContext
    {
        public DadosContext (DbContextOptions<DadosContext> options)
            : base(options)
        {
        }

        public DbSet<MarcaEntity> MarcaModel { get; set; }
        public DbSet<SmartphoneEntity> SmartphoneModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MarcaConfiguration());
            modelBuilder.ApplyConfiguration(new SmartphoneConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
