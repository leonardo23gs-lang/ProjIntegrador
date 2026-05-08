using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.Models
{
    public class Disciplina
    {
        public int IdDisciplina { get; set; }
        public string NomeDisciplina { get; set; } = string.Empty;
        public int IdCoordenador { get; set; }

        // Navegação para o coordenador (Usuario)
        public Usuario Coordenador { get; set; } = null!;

        // Relacionamento N:N com Software
        public ICollection<DisciplinaSoftware> DisciplinaSoftwares { get; set; }
            = new List<DisciplinaSoftware>();
    }
}