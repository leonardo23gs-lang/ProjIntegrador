using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.Models
{
    public class Software
    {
        public int IdSoftware { get; set; }
        public string NomeSoftware { get; set; } = string.Empty;
        public string VersaoSoftware { get; set; } = string.Empty;

        public ICollection<LaboratorioSoftware> LaboratorioSoftwares { get; set; } 
            = new List<LaboratorioSoftware>();
    }
}