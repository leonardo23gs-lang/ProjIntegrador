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
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<DisciplinaSoftware> DisciplinaSoftwares { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Alocacao> Alocacoes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LaboratorioSoftware>()
                .HasKey(ls => new { ls.IdLaboratorio, ls.IdSoftware });
            modelBuilder.Entity<Laboratorio>().ToTable("Laboratorio");
            modelBuilder.Entity<Software>().ToTable("Software");
            modelBuilder.Entity<LaboratorioSoftware>().ToTable("Laboratorio_Software");
            modelBuilder.Entity<DisciplinaSoftware>()
            .HasKey(ds => new { ds.IdDisciplina, ds.IdSoftware });
            modelBuilder.Entity<Disciplina>().ToTable("Disciplina");
            modelBuilder.Entity<DisciplinaSoftware>().ToTable("Disciplina_Software");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Perfil>().ToTable("Perfil");
            modelBuilder.Entity<Turma>().ToTable("Turmas");
            modelBuilder.Entity<Alocacao>().ToTable("Alocacao");
        }
    }
}