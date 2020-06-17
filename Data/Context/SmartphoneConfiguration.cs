using CRUD_Smartphone_Marca.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class SmartphoneConfiguration : IEntityTypeConfiguration<SmartphoneEntity>
    {
        public void Configure(EntityTypeBuilder<SmartphoneEntity> builder)
        {
            builder
                .HasIndex(x => x.Modelo)
                .IsUnique();
        }
    }
}
