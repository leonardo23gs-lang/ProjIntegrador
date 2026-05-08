using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Perfil;

namespace SistemaAlocacaoLab.API.Services
{
    public interface IPerfilService
    {
        Task<IEnumerable<PerfilResponseDto>> GetAllAsync();
        Task<PerfilResponseDto?> GetByIdAsync(int id);
        Task<PerfilResponseDto> CreateAsync(PerfilRequestDto dto);
        Task<PerfilResponseDto?> UpdateAsync(int id, PerfilRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}