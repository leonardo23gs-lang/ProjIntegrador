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
            // PERFIL
            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(p => p.IdPerfil);
                entity.ToTable("Perfil");
                entity.Property(p => p.IdPerfil).HasColumnName("id_perfil");
                entity.Property(p => p.TipoPerfil).HasColumnName("tipo_perfil");
            });

            // USUARIO
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.IdUsuario);
                entity.ToTable("Usuario");
                entity.Property(u => u.IdUsuario).HasColumnName("id_usuario");
                entity.Property(u => u.NomeUsuario).HasColumnName("nome_usuario");
                entity.Property(u => u.EmailUsuario).HasColumnName("email_usuario");
                entity.Property(u => u.SenhaUsuario).HasColumnName("senha_usuario");
                entity.Property(u => u.IdPerfil).HasColumnName("id_perfil");

                entity.HasOne(u => u.Perfil)
                      .WithMany(p => p.Usuarios)
                      .HasForeignKey(u => u.IdPerfil);
            });

            // SOFTWARE
            modelBuilder.Entity<Software>(entity =>
            {
                entity.HasKey(s => s.IdSoftware);
                entity.ToTable("Software");
                entity.Property(s => s.IdSoftware).HasColumnName("id_software");
                entity.Property(s => s.NomeSoftware).HasColumnName("nome_software");
                entity.Property(s => s.VersaoSoftware).HasColumnName("versao_software");
            });

            // LABORATORIO
            modelBuilder.Entity<Laboratorio>(entity =>
            {
                entity.HasKey(l => l.IdLaboratorio);
                entity.ToTable("Laboratorio");
                entity.Property(l => l.IdLaboratorio).HasColumnName("id_laboratorio");
                entity.Property(l => l.NomeLaboratorio).HasColumnName("nome_laboratorio");
                entity.Property(l => l.QtdComputadores).HasColumnName("qtd_computadores");
            });

            // LABORATORIO_SOFTWARE
            modelBuilder.Entity<LaboratorioSoftware>(entity =>
            {
                entity.HasKey(ls => new { ls.IdLaboratorio, ls.IdSoftware });
                entity.ToTable("Laboratorio_Software");
                entity.Property(ls => ls.IdLaboratorio).HasColumnName("id_laboratorio");
                entity.Property(ls => ls.IdSoftware).HasColumnName("id_software");

                entity.HasOne(ls => ls.Laboratorio)
                      .WithMany(l => l.LaboratorioSoftwares)
                      .HasForeignKey(ls => ls.IdLaboratorio);

                entity.HasOne(ls => ls.Software)
                      .WithMany(s => s.LaboratorioSoftwares)
                      .HasForeignKey(ls => ls.IdSoftware);
            });

            // DISCIPLINA
            modelBuilder.Entity<Disciplina>(entity =>
            {
                entity.HasKey(d => d.IdDisciplina);
                entity.ToTable("Disciplina");
                entity.Property(d => d.IdDisciplina).HasColumnName("id_disciplina");
                entity.Property(d => d.NomeDisciplina).HasColumnName("nome_disciplina");
                entity.Property(d => d.IdCoordenador).HasColumnName("id_coordenador");

                entity.HasOne(d => d.Coordenador)
                      .WithMany(u => u.Disciplinas)
                      .HasForeignKey(d => d.IdCoordenador);
            });

            // DISCIPLINA_SOFTWARE
            modelBuilder.Entity<DisciplinaSoftware>(entity =>
            {
                entity.HasKey(ds => new { ds.IdDisciplina, ds.IdSoftware });
                entity.ToTable("Disciplina_Software");
                entity.Property(ds => ds.IdDisciplina).HasColumnName("id_disciplina");
                entity.Property(ds => ds.IdSoftware).HasColumnName("id_software");

                entity.HasOne(ds => ds.Disciplina)
                      .WithMany(d => d.DisciplinaSoftwares)
                      .HasForeignKey(ds => ds.IdDisciplina);

                entity.HasOne(ds => ds.Software)
                      .WithMany(s => s.DisciplinaSoftwares)
                      .HasForeignKey(ds => ds.IdSoftware);
            });

            // TURMA
            modelBuilder.Entity<Turma>(entity =>
            {
                entity.HasKey(t => t.IdTurma);
                entity.ToTable("Turmas");
                entity.Property(t => t.IdTurma).HasColumnName("id_turma");
                entity.Property(t => t.QuantidadeAlunos).HasColumnName("quantidade_alunos");
                entity.Property(t => t.HorarioInicio).HasColumnName("horario_inicio");
                entity.Property(t => t.HorarioFim).HasColumnName("horario_fim");
                entity.Property(t => t.IdDisciplina).HasColumnName("id_disciplina");

                entity.HasOne(t => t.Disciplina)
                      .WithMany()
                      .HasForeignKey(t => t.IdDisciplina);
            });

            // ALOCACAO
            modelBuilder.Entity<Alocacao>(entity =>
            {
                entity.HasKey(a => a.IdAlocacao);
                entity.ToTable("Alocacao");
                entity.Property(a => a.IdAlocacao).HasColumnName("id_alocacao");
                entity.Property(a => a.Status).HasColumnName("status");
                entity.Property(a => a.IdTurma).HasColumnName("id_turma");
                entity.Property(a => a.IdLaboratorio).HasColumnName("id_laboratorio");
                entity.Property(a => a.IdCoordenador).HasColumnName("id_coordenador");

                entity.HasOne(a => a.Turma)
                      .WithMany()
                      .HasForeignKey(a => a.IdTurma);

                entity.HasOne(a => a.Laboratorio)
                      .WithMany()
                      .HasForeignKey(a => a.IdLaboratorio);

                entity.HasOne(a => a.Coordenador)
                      .WithMany()
                      .HasForeignKey(a => a.IdCoordenador);
            });
        }
    }
}