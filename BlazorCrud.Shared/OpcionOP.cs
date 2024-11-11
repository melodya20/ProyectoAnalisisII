using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
   public class OpcionOP
    {
        public int IdOpcion { get; set; }

        public int IdMenu { get; set; }

        public string Nombre { get; set; } = null!;

        public int OrdenMenu { get; set; }

        public string Pagina { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public MenuMN? Menu { get; set; }


    }
}
