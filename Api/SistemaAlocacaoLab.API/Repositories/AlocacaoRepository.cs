using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaAlocacaoLab.API.Data;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public class AlocacaoRepository : IAlocacaoRepository
    {
        private readonly AppDbContext _context;

        public AlocacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alocacao>> GetAllAsync()
        {
            return await _context.Alocacoes
                .Include(a => a.Turma).ThenInclude(t => t.Disciplina)
                .Include(a => a.Laboratorio)
                .Include(a => a.Coordenador)
                .ToListAsync();
        }

        public async Task<Alocacao?> GetByIdAsync(int id)
        {
            return await _context.Alocacoes
                .Include(a => a.Turma).ThenInclude(t => t.Disciplina)
                .Include(a => a.Laboratorio)
                .Include(a => a.Coordenador)
                .FirstOrDefaultAsync(a => a.IdAlocacao == id);
        }

        public async Task<bool> TurmaExisteAsync(int idTurma) =>
            await _context.Turmas.AnyAsync(t => t.IdTurma == idTurma);

        public async Task<bool> LaboratorioExisteAsync(int idLaboratorio) =>
            await _context.Laboratorios.AnyAsync(l => l.IdLaboratorio == idLaboratorio);

        public async Task<bool> CoordenadorExisteAsync(int idCoordenador) =>
            await _context.Usuarios.AnyAsync(u => u.IdUsuario == idCoordenador);

        public async Task<bool> TurmaJaAlocadaAsync(int idTurma) =>
            await _context.Alocacoes.AnyAsync(a => a.IdTurma == idTurma);

        // Verifica se o laboratório já tem uma alocação no mesmo horário
        public async Task<bool> ConflitoDeHorarioAsync(int idLaboratorio, int idTurma)
        {
            var turma = await _context.Turmas.FindAsync(idTurma);
            if (turma == null) return false;

            return await _context.Alocacoes
                .Include(a => a.Turma)
                .AnyAsync(a =>
                    a.IdLaboratorio == idLaboratorio &&
                    a.Turma.HorarioInicio < turma.HorarioFim &&
                    a.Turma.HorarioFim > turma.HorarioInicio
                );
        }

        public async Task<Turma?> GetTurmaAsync(int idTurma) =>
            await _context.Turmas.FindAsync(idTurma);

        public async Task<Laboratorio?> GetLaboratorioAsync(int idLaboratorio) =>
            await _context.Laboratorios.FindAsync(idLaboratorio);

        public async Task AddAsync(Alocacao alocacao) =>
            await _context.Alocacoes.AddAsync(alocacao);

        public async Task UpdateAsync(Alocacao alocacao) =>
            _context.Alocacoes.Update(alocacao);

        public async Task DeleteAsync(Alocacao alocacao) =>
            _context.Alocacoes.Remove(alocacao);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}