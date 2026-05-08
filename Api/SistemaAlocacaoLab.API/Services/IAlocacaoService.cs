using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Alocacao;

namespace SistemaAlocacaoLab.API.Services
{
    public interface IAlocacaoService
    {
        Task<IEnumerable<AlocacaoResponseDto>> GetAllAsync();
        Task<AlocacaoResponseDto?> GetByIdAsync(int id);
        Task<AlocacaoResponseDto> CreateAsync(AlocacaoRequestDto dto);
        Task<AlocacaoResponseDto?> UpdateStatusAsync(int id, string novoStatus);
        Task<bool> DeleteAsync(int id);
    }
}