using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaAlocacaoLab.API.Data;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AppDbContext _context;

        public TurmaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Turma>> GetAllAsync()
        {
            return await _context.Turmas
                .Include(t => t.Disciplina)
                .ToListAsync();
        }

        public async Task<Turma?> GetByIdAsync(int id)
        {
            return await _context.Turmas
                .Include(t => t.Disciplina)
                .FirstOrDefaultAsync(t => t.IdTurma == id);
        }

        public async Task<bool> DisciplinaExisteAsync(int idDisciplina)
        {
            return await _context.Disciplinas
                .AnyAsync(d => d.IdDisciplina == idDisciplina);
        }

        public async Task AddAsync(Turma turma)
        {
            await _context.Turmas.AddAsync(turma);
        }

        public async Task UpdateAsync(Turma turma)
        {
            _context.Turmas.Update(turma);
        }

        public async Task DeleteAsync(Turma turma)
        {
            _context.Turmas.Remove(turma);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}