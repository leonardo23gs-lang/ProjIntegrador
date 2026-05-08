using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public interface ITurmaRepository
    {
        Task<IEnumerable<Turma>> GetAllAsync();
        Task<Turma?> GetByIdAsync(int id);
        Task<bool> DisciplinaExisteAsync(int idDisciplina);
        Task AddAsync(Turma turma);
        Task UpdateAsync(Turma turma);
        Task DeleteAsync(Turma turma);
        Task SaveChangesAsync();
    }
}