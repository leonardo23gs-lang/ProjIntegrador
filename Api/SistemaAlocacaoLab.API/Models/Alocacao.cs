using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.Models
{
    public class Alocacao
    {
        public int IdAlocacao { get; set; }
        public string Status { get; set; } = "Pendente";
        public int IdTurma { get; set; }
        public int IdLaboratorio { get; set; }
        public int IdCoordenador { get; set; }

        public Turma Turma { get; set; } = null!;
        public Laboratorio Laboratorio { get; set; } = null!;
        public Usuario Coordenador { get; set; } = null!;
    }
}