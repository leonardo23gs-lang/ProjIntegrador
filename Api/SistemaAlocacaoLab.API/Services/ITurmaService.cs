using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Turma;

namespace SistemaAlocacaoLab.API.Services
{
    public interface ITurmaService
    {
        Task<IEnumerable<TurmaResponseDto>> GetAllAsync();
        Task<TurmaResponseDto?> GetByIdAsync(int id);
        Task<TurmaResponseDto> CreateAsync(TurmaRequestDto dto);
        Task<TurmaResponseDto?> UpdateAsync(int id, TurmaRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}