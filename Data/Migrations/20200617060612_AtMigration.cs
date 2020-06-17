using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AtMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarcaModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 30, nullable: false),
                    Pais = table.Column<string>(maxLength: 20, nullable: false),
                    qtdSmartphone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartphoneModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 30, nullable: false),
                    Modelo = table.Column<string>(maxLength: 30, nullable: false),
                    Lancamento = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<float>(nullable: false),
                    MarcaEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartphoneModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartphoneModel_MarcaModel_MarcaEntityId",
                        column: x => x.MarcaEntityId,
                        principalTable: "MarcaModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartphoneModel_MarcaEntityId",
                table: "SmartphoneModel",
                column: "MarcaEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartphoneModel_Modelo",
                table: "SmartphoneModel",
                column: "Modelo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartphoneModel");

            migrationBuilder.DropTable(
                name: "MarcaModel");
        }
    }
}
