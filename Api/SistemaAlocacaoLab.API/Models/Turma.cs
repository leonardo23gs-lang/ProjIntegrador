using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.Models
{
    public class Turma
    {
        public int IdTurma { get; set; }
        public int QuantidadeAlunos { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public TimeOnly HorarioFim { get; set; }
        public int IdDisciplina { get; set; }

        public Disciplina Disciplina { get; set; } = null!;
    }
}