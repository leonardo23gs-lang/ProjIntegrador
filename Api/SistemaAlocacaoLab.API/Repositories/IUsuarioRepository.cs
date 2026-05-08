using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<bool> EmailJaExisteAsync(string email);
        Task<bool> PerfilExisteAsync(int idPerfil);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario);
        Task SaveChangesAsync();
    }
}