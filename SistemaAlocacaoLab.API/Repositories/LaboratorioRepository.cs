using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaAlocacaoLab.API.Data;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public class LaboratorioRepository : ILaboratorioRepository
    {
        private readonly AppDbContext _context;

        public LaboratorioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Laboratorio>> GetAllAsync()
        {
            return await _context.Laboratorios
                .Include(l => l.LaboratorioSoftwares)
                    .ThenInclude(ls => ls.Software)
                .ToListAsync();
        }

        public async Task<Laboratorio?> GetByIdAsync(int id)
        {
            return await _context.Laboratorios
                .Include(l => l.LaboratorioSoftwares)
                    .ThenInclude(ls => ls.Software)
                .FirstOrDefaultAsync(l => l.IdLaboratorio == id);
        }

        public async Task AddAsync(Laboratorio laboratorio)
        {
            await _context.Laboratorios.AddAsync(laboratorio);
        }

        public async Task UpdateAsync(Laboratorio laboratorio)
        {
            _context.Laboratorios.Update(laboratorio);
        }

        public async Task DeleteAsync(Laboratorio laboratorio)
        {
            _context.Laboratorios.Remove(laboratorio);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}