using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SistemaAlocacaoLab.API.DTOs.Software
{
    public class SoftwareResponseDto
    {
        public int IdSoftware { get; set; }
        public string NomeSoftware { get; set; } = string.Empty;
        public string VersaoSoftware { get; set; } = string.Empty;
    }
}