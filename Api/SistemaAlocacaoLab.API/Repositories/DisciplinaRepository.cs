using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaAlocacaoLab.API.Data;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public class DisciplinaRepository : IDisciplinaRepository
    {
        private readonly AppDbContext _context;

        public DisciplinaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Disciplina>> GetAllAsync()
        {
            return await _context.Disciplinas
                .Include(d => d.Coordenador)
                .Include(d => d.DisciplinaSoftwares)
                    .ThenInclude(ds => ds.Software)
                .ToListAsync();
        }

        public async Task<Disciplina?> GetByIdAsync(int id)
        {
            return await _context.Disciplinas
                .Include(d => d.Coordenador)
                .Include(d => d.DisciplinaSoftwares)
                    .ThenInclude(ds => ds.Software)
                .FirstOrDefaultAsync(d => d.IdDisciplina == id);
        }

        // Verifica se o coordenador existe antes de cadastrar
        public async Task<bool> CoordenadorExisteAsync(int idCoordenador)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.IdUsuario == idCoordenador);
        }

        public async Task AddAsync(Disciplina disciplina)
        {
            await _context.Disciplinas.AddAsync(disciplina);
        }

        public async Task UpdateAsync(Disciplina disciplina)
        {
            _context.Disciplinas.Update(disciplina);
        }

        public async Task DeleteAsync(Disciplina disciplina)
        {
            _context.Disciplinas.Remove(disciplina);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}