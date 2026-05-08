using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SistemaAlocacaoLab.API.DTOs.Usuario;

namespace SistemaAlocacaoLab.API.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponseDto>> GetAllAsync();
        Task<UsuarioResponseDto?> GetByIdAsync(int id);
        Task<UsuarioResponseDto> CreateAsync(UsuarioRequestDto dto);
        Task<UsuarioResponseDto?> UpdateAsync(int id, UsuarioRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}