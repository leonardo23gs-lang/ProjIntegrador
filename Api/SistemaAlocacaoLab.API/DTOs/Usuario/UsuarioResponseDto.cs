using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.DTOs.Usuario
{
    public class UsuarioResponseDto
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public string EmailUsuario { get; set; } = string.Empty;
        public int IdPerfil { get; set; }
        public string TipoPerfil { get; set; } = string.Empty;

        // Senha nunca é devolvida na resposta — segurança básica
    }
}