using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class TipoAccesoTA
    {
        public int IdTipoAcceso { get; set; }

        public string Nombre { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}
