using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.Models
{
    public class Laboratorio
    {
        public int IdLaboratorio { get; set; }
        public string NomeLaboratorio { get; set; } = string.Empty;
        public int QtdComputadores { get; set; }

        public ICollection<LaboratorioSoftware> LaboratorioSoftwares { get; set; } 
            = new List<LaboratorioSoftware>();
    }
}