using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class UsuarioPreguntumUP
    {
        public int IdPregunta { get; set; }

        public string IdUsuario { get; set; } = null!;

        public string Pregunta { get; set; } = null!;

        public string Respuesta { get; set; } = null!;

        public int OrdenPregunta { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public UsuarioUSO? Usuario { get; set; }
    }
}
