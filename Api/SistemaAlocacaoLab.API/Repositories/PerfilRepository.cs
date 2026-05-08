using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaAlocacaoLab.API.Data;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly AppDbContext _context;

        public PerfilRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Perfil>> GetAllAsync()
        {
            return await _context.Perfis.ToListAsync();
        }

        public async Task<Perfil?> GetByIdAsync(int id)
        {
            return await _context.Perfis
                .FirstOrDefaultAsync(p => p.IdPerfil == id);
        }

        public async Task AddAsync(Perfil perfil)
        {
            await _context.Perfis.AddAsync(perfil);
        }

        public async Task UpdateAsync(Perfil perfil)
        {
            _context.Perfis.Update(perfil);
        }

        public async Task DeleteAsync(Perfil perfil)
        {
            _context.Perfis.Remove(perfil);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}