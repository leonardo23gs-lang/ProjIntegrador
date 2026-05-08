using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Alocacao;
using SistemaAlocacaoLab.API.Models;
using SistemaAlocacaoLab.API.Repositories;

namespace SistemaAlocacaoLab.API.Services
{
    public class AlocacaoService : IAlocacaoService
    {
        private readonly IAlocacaoRepository _repository;

        public AlocacaoService(IAlocacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AlocacaoResponseDto>> GetAllAsync()
        {
            var alocacoes = await _repository.GetAllAsync();
            return alocacoes.Select(a => MapToResponseDto(a));
        }

        public async Task<AlocacaoResponseDto?> GetByIdAsync(int id)
        {
            var alocacao = await _repository.GetByIdAsync(id);
            if (alocacao == null) return null;
            return MapToResponseDto(alocacao);
        }

        public async Task<AlocacaoResponseDto> CreateAsync(AlocacaoRequestDto dto)
        {
            if (!await _repository.TurmaExisteAsync(dto.IdTurma))
                throw new ArgumentException("Turma informada não encontrada.");

            if (!await _repository.LaboratorioExisteAsync(dto.IdLaboratorio))
                throw new ArgumentException("Laboratório informado não encontrado.");

            if (!await _repository.CoordenadorExisteAsync(dto.IdCoordenador))
                throw new ArgumentException("Coordenador informado não encontrado.");

            // Regra: cada turma só pode ter uma alocação
            if (await _repository.TurmaJaAlocadaAsync(dto.IdTurma))
                throw new ArgumentException("Esta turma já possui uma alocação.");

            // Regra: controle de conflito de horário
            if (await _repository.ConflitoDeHorarioAsync(dto.IdLaboratorio, dto.IdTurma))
                throw new ArgumentException("Já existe uma alocação neste laboratório para este horário.");

            // Regra de ocupação 2:1 — quantidade de alunos não pode exceder 2x os computadores
            var turma = await _repository.GetTurmaAsync(dto.IdTurma);
            var laboratorio = await _repository.GetLaboratorioAsync(dto.IdLaboratorio);

            if (turma!.QuantidadeAlunos > laboratorio!.QtdComputadores * 2)
                throw new ArgumentException(
                    $"O laboratório suporta no máximo {laboratorio.QtdComputadores * 2} alunos " +
                    $"(2 por computador), mas a turma tem {turma.QuantidadeAlunos} alunos."
                );

            var alocacao = new Alocacao
            {
                Status = "Pendente",
                IdTurma = dto.IdTurma,
                IdLaboratorio = dto.IdLaboratorio,
                IdCoordenador = dto.IdCoordenador
            };

            await _repository.AddAsync(alocacao);
            await _repository.SaveChangesAsync();

            var criada = await _repository.GetByIdAsync(alocacao.IdAlocacao);
            return MapToResponseDto(criada!);
        }

        // Na Alocação o Update é só de status — os dados em si não mudam após criada
        public async Task<AlocacaoResponseDto?> UpdateStatusAsync(int id, string novoStatus)
        {
            var alocacao = await _repository.GetByIdAsync(id);
            if (alocacao == null) return null;

            var statusValidos = new[] { "Pendente", "Aprovada", "Rejeitada", "Cancelada" };
            if (!statusValidos.Contains(novoStatus))
                throw new ArgumentException("Status inválido. Use: Pendente, Aprovada, Rejeitada ou Cancelada.");

            alocacao.Status = novoStatus;

            await _repository.UpdateAsync(alocacao);
            await _repository.SaveChangesAsync();

            var atualizada = await _repository.GetByIdAsync(id);
            return MapToResponseDto(atualizada!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alocacao = await _repository.GetByIdAsync(id);
            if (alocacao == null) return false;

            await _repository.DeleteAsync(alocacao);
            await _repository.SaveChangesAsync();

            return true;
        }

        private AlocacaoResponseDto MapToResponseDto(Alocacao alocacao)
        {
            return new AlocacaoResponseDto
            {
                IdAlocacao = alocacao.IdAlocacao,
                Status = alocacao.Status,
                IdTurma = alocacao.IdTurma,
                NomeDisciplina = alocacao.Turma?.Disciplina?.NomeDisciplina ?? "",
                IdLaboratorio = alocacao.IdLaboratorio,
                NomeLaboratorio = alocacao.Laboratorio?.NomeLaboratorio ?? "",
                IdCoordenador = alocacao.IdCoordenador,
                NomeCoordenador = alocacao.Coordenador?.NomeUsuario ?? "",
                QuantidadeAlunos = alocacao.Turma?.QuantidadeAlunos ?? 0,
                QtdComputadores = alocacao.Laboratorio?.QtdComputadores ?? 0
            };
        }
    }
}