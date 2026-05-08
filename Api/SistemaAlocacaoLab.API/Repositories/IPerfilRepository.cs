using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public interface IPerfilRepository
    {
        Task<IEnumerable<Perfil>> GetAllAsync();
        Task<Perfil?> GetByIdAsync(int id);
        Task AddAsync(Perfil perfil);
        Task UpdateAsync(Perfil perfil);
        Task DeleteAsync(Perfil perfil);
        Task SaveChangesAsync();
    }
}