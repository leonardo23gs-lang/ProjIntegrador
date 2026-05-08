using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlocacaoLab.API.Models
{
    public class Perfil
    {
        public int IdPerfil { get; set; }
        public string TipoPerfil { get; set; } = string.Empty;

        public ICollection<Usuario> Usuarios { get; set; } 
            = new List<Usuario>();
    }
}