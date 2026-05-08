using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Disciplina;
using SistemaAlocacaoLab.API.Models;
using SistemaAlocacaoLab.API.Repositories;

namespace SistemaAlocacaoLab.API.Services
{
    public class DisciplinaService : IDisciplinaService
    {
        private readonly IDisciplinaRepository _repository;

        public DisciplinaService(IDisciplinaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DisciplinaResponseDto>> GetAllAsync()
        {
            var disciplinas = await _repository.GetAllAsync();
            return disciplinas.Select(d => MapToResponseDto(d));
        }

        public async Task<DisciplinaResponseDto?> GetByIdAsync(int id)
        {
            var disciplina = await _repository.GetByIdAsync(id);

            if (disciplina == null)
                return null;

            return MapToResponseDto(disciplina);
        }

        public async Task<DisciplinaResponseDto> CreateAsync(DisciplinaRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NomeDisciplina))
                throw new ArgumentException("O nome da disciplina é obrigatório.");

            if (!await _repository.CoordenadorExisteAsync(dto.IdCoordenador))
                throw new ArgumentException("Coordenador informado não encontrado.");

            var disciplina = new Disciplina
            {
                NomeDisciplina = dto.NomeDisciplina,
                IdCoordenador = dto.IdCoordenador,
                DisciplinaSoftwares = dto.IdsSoftwares.Select(idSoftware =>
                    new DisciplinaSoftware { IdSoftware = idSoftware }
                ).ToList()
            };

            await _repository.AddAsync(disciplina);
            await _repository.SaveChangesAsync();

            // Recarrega para trazer o nome do coordenador
            var criada = await _repository.GetByIdAsync(disciplina.IdDisciplina);
            return MapToResponseDto(criada!);
        }

        public async Task<DisciplinaResponseDto?> UpdateAsync(int id, DisciplinaRequestDto dto)
        {
            var disciplina = await _repository.GetByIdAsync(id);

            if (disciplina == null)
                return null;

            if (string.IsNullOrWhiteSpace(dto.NomeDisciplina))
                throw new ArgumentException("O nome da disciplina é obrigatório.");

            if (!await _repository.CoordenadorExisteAsync(dto.IdCoordenador))
                throw new ArgumentException("Coordenador informado não encontrado.");

            disciplina.NomeDisciplina = dto.NomeDisciplina;
            disciplina.IdCoordenador = dto.IdCoordenador;
            disciplina.DisciplinaSoftwares = dto.IdsSoftwares.Select(idSoftware =>
                new DisciplinaSoftware { IdDisciplina = id, IdSoftware = idSoftware }
            ).ToList();

            await _repository.UpdateAsync(disciplina);
            await _repository.SaveChangesAsync();

            var atualizada = await _repository.GetByIdAsync(id);
            return MapToResponseDto(atualizada!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var disciplina = await _repository.GetByIdAsync(id);

            if (disciplina == null)
                return false;

            await _repository.DeleteAsync(disciplina);
            await _repository.SaveChangesAsync();

            return true;
        }

        private DisciplinaResponseDto MapToResponseDto(Disciplina disciplina)
        {
            return new DisciplinaResponseDto
            {
                IdDisciplina = disciplina.IdDisciplina,
                NomeDisciplina = disciplina.NomeDisciplina,
                IdCoordenador = disciplina.IdCoordenador,
                NomeCoordenador = disciplina.Coordenador?.NomeUsuario ?? "",
                Softwares = disciplina.DisciplinaSoftwares
                    .Select(ds => ds.Software?.NomeSoftware ?? "")
                    .ToList()
            };
        }
    }
}