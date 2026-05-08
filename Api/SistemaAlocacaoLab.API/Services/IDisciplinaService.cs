using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Disciplina;

namespace SistemaAlocacaoLab.API.Services
{
    public interface IDisciplinaService
    {
        Task<IEnumerable<DisciplinaResponseDto>> GetAllAsync();
        Task<DisciplinaResponseDto?> GetByIdAsync(int id);
        Task<DisciplinaResponseDto> CreateAsync(DisciplinaRequestDto dto);
        Task<DisciplinaResponseDto?> UpdateAsync(int id, DisciplinaRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}