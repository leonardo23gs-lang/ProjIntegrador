using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Alocacao
{
    public class AlocacaoResponseDto
    {
        public int IdAlocacao { get; set; }
        public string Status { get; set; } = string.Empty;
        public int IdTurma { get; set; }
        public string NomeDisciplina { get; set; } = string.Empty;
        public int IdLaboratorio { get; set; }
        public string NomeLaboratorio { get; set; } = string.Empty;
        public int IdCoordenador { get; set; }
        public string NomeCoordenador { get; set; } = string.Empty;
        public int QuantidadeAlunos { get; set; }
        public int QtdComputadores { get; set; }
    }
}