using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Laboratorio;

namespace SistemaAlocacaoLab.API.Services
{
    public interface ILaboratorioService
    {
        Task<IEnumerable<LaboratorioResponseDto>> GetAllAsync();
        Task<LaboratorioResponseDto?> GetByIdAsync(int id);
        Task<LaboratorioResponseDto> CreateAsync(LaboratorioRequestDto dto);
        Task<LaboratorioResponseDto?> UpdateAsync(int id, LaboratorioRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}