using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using S2B.SQLite;

namespace S2B.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20160111214652_PrimeiraMigracao")]
    partial class PrimeiraMigracao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("S2B.Models.Domain.Armazenamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Categorias")
                        .IsRequired();

                    b.Property<string>("Comentario")
                        .HasAnnotation("MaxLength", 140);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("S2B.Models.Domain.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArmazenamentoId");

                    b.Property<string>("Comentario")
                        .HasAnnotation("MaxLength", 60);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.Property<int>("Quantidade")
                        .HasAnnotation("MaxLength", 10000);

                    b.Property<DateTime?>("Validade");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("S2B.Models.Domain.Material", b =>
                {
                    b.HasOne("S2B.Models.Domain.Armazenamento")
                        .WithMany()
                        .HasForeignKey("ArmazenamentoId");
                });
        }
    }
}
