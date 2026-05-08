using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Perfil
{
    public class PerfilResponseDto
    {
        public int IdPerfil { get; set; }
        public string TipoPerfil { get; set; } = string.Empty;
    }
}