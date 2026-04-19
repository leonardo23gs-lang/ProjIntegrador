using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Laboratorio
{
    public class LaboratorioResponseDto
    {
        public int IdLaboratorio { get; set; }
        public string NomeLaboratorio { get; set; } = string.Empty;
        public int QtdComputadores { get; set; }

        public int CapacidadeAlunos => QtdComputadores * 2;

        public List<string> Softwares { get; set; } = new List<string>();
    }
}