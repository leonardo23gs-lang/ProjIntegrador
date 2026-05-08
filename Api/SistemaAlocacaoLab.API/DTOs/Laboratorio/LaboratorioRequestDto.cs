using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Laboratorio
{
    public class LaboratorioRequestDto
    {
        public string NomeLaboratorio { get; set; } = string.Empty;
        public int QtdComputadores { get; set; }
        
        public List<int> IdsSoftwares { get; set; } = new List<int>();
    }
}