using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public interface ISoftwareRepository
    {
        Task<IEnumerable<Software>> GetAllAsync();
        Task<Software?> GetByIdAsync(int id);
        Task AddAsync(Software software);
        Task UpdateAsync(Software software);
        Task DeleteAsync(Software software);
        Task SaveChangesAsync();
    }
}