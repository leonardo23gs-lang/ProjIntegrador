using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Alocacao
{
    public class AlocacaoRequestDto
    {
        public int IdTurma { get; set; }
        public int IdLaboratorio { get; set; }
        public int IdCoordenador { get; set; }
    }
}