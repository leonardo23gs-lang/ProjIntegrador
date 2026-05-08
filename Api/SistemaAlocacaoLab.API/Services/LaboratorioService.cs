using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SistemaAlocacaoLab.API.DTOs.Laboratorio;
using SistemaAlocacaoLab.API.Models;
using SistemaAlocacaoLab.API.Repositories;

namespace SistemaAlocacaoLab.API.Services
{
    public class LaboratorioService : ILaboratorioService
    {
        private readonly ILaboratorioRepository _repository;

        public LaboratorioService(ILaboratorioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LaboratorioResponseDto>> GetAllAsync()
        {
            var laboratorios = await _repository.GetAllAsync();
            return laboratorios.Select(l => MapToResponseDto(l));
        }

        public async Task<LaboratorioResponseDto?> GetByIdAsync(int id)
        {
            var laboratorio = await _repository.GetByIdAsync(id);

            if (laboratorio == null)
                return null;

            return MapToResponseDto(laboratorio);
        }

        public async Task<LaboratorioResponseDto> CreateAsync(LaboratorioRequestDto dto)
        {
            if (dto.QtdComputadores <= 0)
                throw new ArgumentException("A quantidade de computadores deve ser maior que zero.");

            if (string.IsNullOrWhiteSpace(dto.NomeLaboratorio))
                throw new ArgumentException("O nome do laboratório é obrigatório.");

            var laboratorio = new Laboratorio
            {
                NomeLaboratorio = dto.NomeLaboratorio,
                QtdComputadores = dto.QtdComputadores,
                LaboratorioSoftwares = dto.IdsSoftwares.Select(idSoftware =>
                    new LaboratorioSoftware { IdSoftware = idSoftware }
                ).ToList()
            };

            await _repository.AddAsync(laboratorio);
            await _repository.SaveChangesAsync();

            return MapToResponseDto(laboratorio);
        }

        public async Task<LaboratorioResponseDto?> UpdateAsync(int id, LaboratorioRequestDto dto)
        {
            var laboratorio = await _repository.GetByIdAsync(id);

            if (laboratorio == null)
                return null;

            if (dto.QtdComputadores <= 0)
                throw new ArgumentException("A quantidade de computadores deve ser maior que zero.");

            if (string.IsNullOrWhiteSpace(dto.NomeLaboratorio))
                throw new ArgumentException("O nome do laboratório é obrigatório.");

            laboratorio.NomeLaboratorio = dto.NomeLaboratorio;
            laboratorio.QtdComputadores = dto.QtdComputadores;

            laboratorio.LaboratorioSoftwares = dto.IdsSoftwares.Select(idSoftware =>
                new LaboratorioSoftware { IdLaboratorio = id, IdSoftware = idSoftware }
            ).ToList();

            await _repository.UpdateAsync(laboratorio);
            await _repository.SaveChangesAsync();

            return MapToResponseDto(laboratorio);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var laboratorio = await _repository.GetByIdAsync(id);

            if (laboratorio == null)
                return false;

            await _repository.DeleteAsync(laboratorio);
            await _repository.SaveChangesAsync();

            return true;
        }

        private LaboratorioResponseDto MapToResponseDto(Laboratorio laboratorio)
        {
            return new LaboratorioResponseDto
            {
                IdLaboratorio = laboratorio.IdLaboratorio,
                NomeLaboratorio = laboratorio.NomeLaboratorio,
                QtdComputadores = laboratorio.QtdComputadores,
                Softwares = laboratorio.LaboratorioSoftwares
                    .Select(ls => ls.Software?.NomeSoftware ?? "")
                    .ToList()
            };
        }
    }
}