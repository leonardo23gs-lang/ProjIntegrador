using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaAlocacaoLab.API.Data;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly AppDbContext _context;

        public SoftwareRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Software>> GetAllAsync()
        {
            return await _context.Softwares.ToListAsync();
        }

        public async Task<Software?> GetByIdAsync(int id)
        {
            return await _context.Softwares
                .FirstOrDefaultAsync(s => s.IdSoftware == id);
        }

        public async Task AddAsync(Software software)
        {
            await _context.Softwares.AddAsync(software);
        }

        public async Task UpdateAsync(Software software)
        {
            _context.Softwares.Update(software);
        }

        public async Task DeleteAsync(Software software)
        {
            _context.Softwares.Remove(software);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}