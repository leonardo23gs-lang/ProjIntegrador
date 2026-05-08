using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SistemaAlocacaoLab.API.DTOs.Perfil;
using SistemaAlocacaoLab.API.Models;
using SistemaAlocacaoLab.API.Repositories;
namespace SistemaAlocacaoLab.API.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _repository;

        public PerfilService(IPerfilRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PerfilResponseDto>> GetAllAsync()
        {
            var perfis = await _repository.GetAllAsync();
            return perfis.Select(p => MapToResponseDto(p));
        }

        public async Task<PerfilResponseDto?> GetByIdAsync(int id)
        {
            var perfil = await _repository.GetByIdAsync(id);
            if (perfil == null) return null;
            return MapToResponseDto(perfil);
        }

        public async Task<PerfilResponseDto> CreateAsync(PerfilRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TipoPerfil))
                throw new ArgumentException("O tipo do perfil é obrigatório.");

            // Regra de negócio: só aceita os tipos definidos no banco
            var tiposValidos = new[] { "C", "T", "D" };
            if (!tiposValidos.Contains(dto.TipoPerfil))
                throw new ArgumentException("Tipo de perfil inválido. Use C (Coordenador), T (TI) ou D (Diretor).");

            var perfil = new Perfil { TipoPerfil = dto.TipoPerfil };

            await _repository.AddAsync(perfil);
            await _repository.SaveChangesAsync();

            return MapToResponseDto(perfil);
        }

        public async Task<PerfilResponseDto?> UpdateAsync(int id, PerfilRequestDto dto)
        {
            var perfil = await _repository.GetByIdAsync(id);
            if (perfil == null) return null;

            var tiposValidos = new[] { "C", "T", "D" };
            if (!tiposValidos.Contains(dto.TipoPerfil))
                throw new ArgumentException("Tipo de perfil inválido. Use C (Coordenador), T (TI) ou D (Diretor).");

            perfil.TipoPerfil = dto.TipoPerfil;

            await _repository.UpdateAsync(perfil);
            await _repository.SaveChangesAsync();

            return MapToResponseDto(perfil);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var perfil = await _repository.GetByIdAsync(id);
            if (perfil == null) return false;

            await _repository.DeleteAsync(perfil);
            await _repository.SaveChangesAsync();

            return true;
        }

        private PerfilResponseDto MapToResponseDto(Perfil perfil)
        {
            return new PerfilResponseDto
            {
                IdPerfil = perfil.IdPerfil,
                TipoPerfil = perfil.TipoPerfil
            };
        }
    }
}