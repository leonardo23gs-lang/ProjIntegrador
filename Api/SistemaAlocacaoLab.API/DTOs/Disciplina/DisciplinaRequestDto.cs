using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Disciplina
{
    public class DisciplinaRequestDto
    {
        public string NomeDisciplina { get; set; } = string.Empty;
        public int IdCoordenador { get; set; }
        public List<int> IdsSoftwares { get; set; } = new List<int>();
    }
}