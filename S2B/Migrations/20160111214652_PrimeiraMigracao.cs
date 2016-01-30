using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace S2B.Migrations
{
    public partial class PrimeiraMigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Armazenamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Categorias = table.Column<string>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armazenamento", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArmazenamentoId = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Validade = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Material_Armazenamento_ArmazenamentoId",
                        column: x => x.ArmazenamentoId,
                        principalTable: "Armazenamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Material");
            migrationBuilder.DropTable("Armazenamento");
        }
    }
}
