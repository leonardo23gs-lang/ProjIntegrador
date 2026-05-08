using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Software;
using SistemaAlocacaoLab.API.Models;
using SistemaAlocacaoLab.API.Repositories;

namespace SistemaAlocacaoLab.API.Services
{
    public class SoftwareService : ISoftwareService
    {
        private readonly ISoftwareRepository _repository;

        public SoftwareService(ISoftwareRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SoftwareResponseDto>> GetAllAsync()
        {
            var softwares = await _repository.GetAllAsync();
            return softwares.Select(s => MapToResponseDto(s));
        }

        public async Task<SoftwareResponseDto?> GetByIdAsync(int id)
        {
            var software = await _repository.GetByIdAsync(id);
            if (software == null) return null;
            return MapToResponseDto(software);
        }

        public async Task<SoftwareResponseDto> CreateAsync(SoftwareRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NomeSoftware))
                throw new ArgumentException("O nome do software é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.VersaoSoftware))
                throw new ArgumentException("A versão do software é obrigatória.");

            var software = new Software
            {
                NomeSoftware = dto.NomeSoftware,
                VersaoSoftware = dto.VersaoSoftware
            };

            await _repository.AddAsync(software);
            await _repository.SaveChangesAsync();

            return MapToResponseDto(software);
        }

        public async Task<SoftwareResponseDto?> UpdateAsync(int id, SoftwareRequestDto dto)
        {
            var software = await _repository.GetByIdAsync(id);
            if (software == null) return null;

            if (string.IsNullOrWhiteSpace(dto.NomeSoftware))
                throw new ArgumentException("O nome do software é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.VersaoSoftware))
                throw new ArgumentException("A versão do software é obrigatória.");

            software.NomeSoftware = dto.NomeSoftware;
            software.VersaoSoftware = dto.VersaoSoftware;

            await _repository.UpdateAsync(software);
            await _repository.SaveChangesAsync();

            return MapToResponseDto(software);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var software = await _repository.GetByIdAsync(id);
            if (software == null) return false;

            await _repository.DeleteAsync(software);
            await _repository.SaveChangesAsync();

            return true;
        }

        private SoftwareResponseDto MapToResponseDto(Software software)
        {
            return new SoftwareResponseDto
            {
                IdSoftware = software.IdSoftware,
                NomeSoftware = software.NomeSoftware,
                VersaoSoftware = software.VersaoSoftware
            };
        }
    }
}