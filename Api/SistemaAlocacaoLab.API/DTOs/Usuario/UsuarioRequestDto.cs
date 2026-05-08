using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Usuario
{
    public class UsuarioRequestDto
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string EmailUsuario { get; set; } = string.Empty;
        public string SenhaUsuario { get; set; } = string.Empty;
        public int IdPerfil { get; set; }
    }
}