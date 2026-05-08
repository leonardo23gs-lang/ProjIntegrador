using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Turma;
using SistemaAlocacaoLab.API.Models;
using SistemaAlocacaoLab.API.Repositories;

namespace SistemaAlocacaoLab.API.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _repository;

        public TurmaService(ITurmaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TurmaResponseDto>> GetAllAsync()
        {
            var turmas = await _repository.GetAllAsync();
            return turmas.Select(t => MapToResponseDto(t));
        }

        public async Task<TurmaResponseDto?> GetByIdAsync(int id)
        {
            var turma = await _repository.GetByIdAsync(id);
            if (turma == null) return null;
            return MapToResponseDto(turma);
        }

        public async Task<TurmaResponseDto> CreateAsync(TurmaRequestDto dto)
        {
            if (dto.QuantidadeAlunos <= 0)
                throw new ArgumentException("A quantidade de alunos deve ser maior que zero.");

            // Regra de negócio: horário fim deve ser maior que início
            if (dto.HorarioFim <= dto.HorarioInicio)
                throw new ArgumentException("O horário de fim deve ser maior que o horário de início.");

            if (!await _repository.DisciplinaExisteAsync(dto.IdDisciplina))
                throw new ArgumentException("Disciplina informada não encontrada.");

            var turma = new Turma
            {
                QuantidadeAlunos = dto.QuantidadeAlunos,
                HorarioInicio = dto.HorarioInicio,
                HorarioFim = dto.HorarioFim,
                IdDisciplina = dto.IdDisciplina
            };

            await _repository.AddAsync(turma);
            await _repository.SaveChangesAsync();

            var criada = await _repository.GetByIdAsync(turma.IdTurma);
            return MapToResponseDto(criada!);
        }

        public async Task<TurmaResponseDto?> UpdateAsync(int id, TurmaRequestDto dto)
        {
            var turma = await _repository.GetByIdAsync(id);
            if (turma == null) return null;

            if (dto.QuantidadeAlunos <= 0)
                throw new ArgumentException("A quantidade de alunos deve ser maior que zero.");

            if (dto.HorarioFim <= dto.HorarioInicio)
                throw new ArgumentException("O horário de fim deve ser maior que o horário de início.");

            if (!await _repository.DisciplinaExisteAsync(dto.IdDisciplina))
                throw new ArgumentException("Disciplina informada não encontrada.");

            turma.QuantidadeAlunos = dto.QuantidadeAlunos;
            turma.HorarioInicio = dto.HorarioInicio;
            turma.HorarioFim = dto.HorarioFim;
            turma.IdDisciplina = dto.IdDisciplina;

            await _repository.UpdateAsync(turma);
            await _repository.SaveChangesAsync();

            var atualizada = await _repository.GetByIdAsync(id);
            return MapToResponseDto(atualizada!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var turma = await _repository.GetByIdAsync(id);
            if (turma == null) return false;

            await _repository.DeleteAsync(turma);
            await _repository.SaveChangesAsync();

            return true;
        }

        private TurmaResponseDto MapToResponseDto(Turma turma)
        {
            return new TurmaResponseDto
            {
                IdTurma = turma.IdTurma,
                QuantidadeAlunos = turma.QuantidadeAlunos,
                HorarioInicio = turma.HorarioInicio,
                HorarioFim = turma.HorarioFim,
                IdDisciplina = turma.IdDisciplina,
                NomeDisciplina = turma.Disciplina?.NomeDisciplina ?? ""
            };
        }
    }
}