using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class EmpresaEM
    {

        public int IdEmpresa { get; set; }

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Nit { get; set; } = null!;

        public int? PasswordCantidadMayusculas { get; set; }

        public int? PasswordCantidadMinusculas { get; set; }

        public int? PasswordCantidadCaracteresEspeciales { get; set; }

        public int PasswordCantidadCaducidadDias { get; set; }

        public int? PasswordLargo { get; set; }

        public int? PasswordIntentosAntesDeBloquear { get; set; }

        public int? PasswordCantidadNumeros { get; set; }

        public int? PasswordCantidadPreguntasValidar { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}
