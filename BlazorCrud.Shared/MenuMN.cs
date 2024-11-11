using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class MenuMN
    {
        public int IdMenu { get; set; }

        public int IdModulo { get; set; }

        public string Nombre { get; set; } = null!;

        public int OrdenMenu { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public ModuloMD? Modulo { get; set; }
       

    }
}
