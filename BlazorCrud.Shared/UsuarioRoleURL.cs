using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class UsuarioRoleURL
    {
        public string IdUsuario { get; set; } = null!;

        public int IdRole { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public RoleRL? Role { get; set; }
        public UsuarioUSO? Usuario { get; set; }
    }
}
