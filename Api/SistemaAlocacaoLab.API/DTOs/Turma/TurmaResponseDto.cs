using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Turma
{
    public class TurmaResponseDto
    {
        public int IdTurma { get; set; }
        public int QuantidadeAlunos { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public TimeOnly HorarioFim { get; set; }
        public int IdDisciplina { get; set; }
        public string NomeDisciplina { get; set; } = string.Empty;
    }
}