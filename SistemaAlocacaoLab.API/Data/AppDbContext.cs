using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Cada DbSet representa uma tabela que o EF vai gerenciar
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<LaboratorioSoftware> LaboratorioSoftwares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura a chave primária composta da tabela associativa
            modelBuilder.Entity<LaboratorioSoftware>()
                .HasKey(ls => new { ls.IdLaboratorio, ls.IdSoftware });

            // Mapeia os nomes reais das tabelas no banco
            modelBuilder.Entity<Laboratorio>().ToTable("Laboratorio");
            modelBuilder.Entity<Software>().ToTable("Software");
            modelBuilder.Entity<LaboratorioSoftware>().ToTable("Laboratorio_Software");
        }
    }
}