using Microsoft.Data.Entity;
using S2B.Models.Domain;

namespace S2B.SQLite
{
    /// <summary>
    /// Classe Contexto tem como objetivo fazer o banco de dados utilizando o Entity Framework.
    /// Versão do Entity Framework 7.0 BETA.
    /// </summary>
    class Contexto : DbContext
    {
        public DbSet<Armazenamento> Armazenamentos { get; set; }
        public DbSet<Material> Materiais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=Armazenamento.db");
        }
    }
}
