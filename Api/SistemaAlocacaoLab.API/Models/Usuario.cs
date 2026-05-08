using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public string EmailUsuario { get; set; } = string.Empty;
        public string SenhaUsuario { get; set; } = string.Empty;
        public int IdPerfil { get; set; }

        // Navegação para Perfil
        public Perfil Perfil { get; set; } = null!;

        public ICollection<Disciplina> Disciplinas { get; set; }
            = new List<Disciplina>();
    }
}