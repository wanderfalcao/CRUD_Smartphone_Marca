using CRUD_Smartphone_Marca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class MarcaConfiguration : IEntityTypeConfiguration<MarcaEntity>
    {
        public void Configure(EntityTypeBuilder<MarcaEntity> builder)
        {
            builder
                .HasIndex(x => x.Nome)
                .IsUnique();
        }
    }
}
