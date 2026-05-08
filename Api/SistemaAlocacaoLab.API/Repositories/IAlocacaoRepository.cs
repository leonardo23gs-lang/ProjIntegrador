using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.Models;

namespace SistemaAlocacaoLab.API.Repositories
{
    public interface IAlocacaoRepository
    {
        Task<IEnumerable<Alocacao>> GetAllAsync();
        Task<Alocacao?> GetByIdAsync(int id);
        Task<bool> TurmaExisteAsync(int idTurma);
        Task<bool> LaboratorioExisteAsync(int idLaboratorio);
        Task<bool> CoordenadorExisteAsync(int idCoordenador);
        Task<bool> TurmaJaAlocadaAsync(int idTurma);
        Task<bool> ConflitoDeHorarioAsync(int idLaboratorio, int idTurma);
        Task<Turma?> GetTurmaAsync(int idTurma);
        Task<Laboratorio?> GetLaboratorioAsync(int idLaboratorio);
        Task AddAsync(Alocacao alocacao);
        Task UpdateAsync(Alocacao alocacao);
        Task DeleteAsync(Alocacao alocacao);
        Task SaveChangesAsync();
    }
}