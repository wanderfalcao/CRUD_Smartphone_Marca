﻿// <auto-generated />
using System;
using CRUD_Smartphone_Marca.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(DadosContext))]
    [Migration("20200617060612_AtMigration")]
    partial class AtMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CRUD_Smartphone_Marca.Domain.Models.MarcaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("qtdSmartphone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MarcaModel");
                });

            modelBuilder.Entity("CRUD_Smartphone_Marca.Model.Entities.SmartphoneEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Lancamento")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MarcaEntityId")
                        .HasColumnType("int");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<float>("Valor")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MarcaEntityId");

                    b.HasIndex("Modelo")
                        .IsUnique();

                    b.ToTable("SmartphoneModel");
                });

            modelBuilder.Entity("CRUD_Smartphone_Marca.Model.Entities.SmartphoneEntity", b =>
                {
                    b.HasOne("CRUD_Smartphone_Marca.Domain.Models.MarcaEntity", "Marca")
                        .WithMany("Smartphone")
                        .HasForeignKey("MarcaEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}
