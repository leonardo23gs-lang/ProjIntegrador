using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Disciplina
{
    public class DisciplinaResponseDto
    {
        public int IdDisciplina { get; set; }
        public string NomeDisciplina { get; set; } = string.Empty;
        public int IdCoordenador { get; set; }
        public string NomeCoordenador { get; set; } = string.Empty;
        public List<string> Softwares { get; set; } = new List<string>();
    }
}