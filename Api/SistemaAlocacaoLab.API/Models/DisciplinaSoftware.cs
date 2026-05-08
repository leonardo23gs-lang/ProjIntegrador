using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.Models
{
    public class DisciplinaSoftware
    {
        public int IdDisciplina { get; set; }
        public Disciplina Disciplina { get; set; } = null!;

        public int IdSoftware { get; set; }
        public Software Software { get; set; } = null!;
    }
}