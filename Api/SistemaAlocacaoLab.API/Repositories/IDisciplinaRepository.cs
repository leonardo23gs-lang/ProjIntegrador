using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public interface IDisciplinaRepository
    {
        Task<IEnumerable<Disciplina>> GetAllAsync();
        Task<Disciplina?> GetByIdAsync(int id);
        Task<bool> CoordenadorExisteAsync(int idCoordenador);
        Task AddAsync(Disciplina disciplina);
        Task UpdateAsync(Disciplina disciplina);
        Task DeleteAsync(Disciplina disciplina);
        Task SaveChangesAsync();
    }
}