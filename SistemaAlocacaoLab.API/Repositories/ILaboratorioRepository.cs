using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public interface ILaboratorioRepository
    {
        Task<IEnumerable<Laboratorio>> GetAllAsync();
        Task<Laboratorio?> GetByIdAsync(int id);
        Task AddAsync(Laboratorio laboratorio);
        Task UpdateAsync(Laboratorio laboratorio);
        Task DeleteAsync(Laboratorio laboratorio);
        Task SaveChangesAsync();
    }
}