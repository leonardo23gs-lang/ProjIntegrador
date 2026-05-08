using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Software;

namespace SistemaAlocacaoLab.API.Services
{
    public interface ISoftwareService
    {
        Task<IEnumerable<SoftwareResponseDto>> GetAllAsync();
        Task<SoftwareResponseDto?> GetByIdAsync(int id);
        Task<SoftwareResponseDto> CreateAsync(SoftwareRequestDto dto);
        Task<SoftwareResponseDto?> UpdateAsync(int id, SoftwareRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}