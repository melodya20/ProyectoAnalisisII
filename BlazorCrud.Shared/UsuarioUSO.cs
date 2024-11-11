using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class UsuarioUSO
    {
        public string? IdUsuario { get; set; }

        //[Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }

        //[Required(ErrorMessage = "El apellido es obligatorio.")]
        public string? Apellido { get; set; }

        //[Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateOnly FechaNacimiento { get; set; }

        //[Required(ErrorMessage = "El estado del usuario es obligatorio.")]
        public int IdStatusUsuario { get; set; }

        //[Required(ErrorMessage = "El password es obligatorio.")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        //    ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluyendo una letra mayúscula, una minúscula, un número y un carácter especial.")]
        public string? Password { get; set; }

        //[Required(ErrorMessage = "El género es obligatorio.")]
        public int IdGenero { get; set; }

        public DateTime? UltimaFechaIngreso { get; set; }

        public int? IntentosDeAcceso { get; set; }

        public string? SesionActual { get; set; }

        public bool _SesionActual
        {
            get => SesionActual == "true";
            set => SesionActual = value ? "true" : "false";
        }

        public DateTime UltimaFechaCambioPassword { get; set; }

        //[Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        //[EmailAddress(ErrorMessage = "Formato de correo electrónico inválido.")]
        public string CorreoElectronico { get; set; }

        public int? RequiereCambiarPassword { get; set; }

        public bool _RequiereCambiarPassword
        {
            get => RequiereCambiarPassword == 1;
            set => RequiereCambiarPassword = value ? 1 : 0;
        }

        public byte[]? Fotografia { get; set; }


        //[Required(ErrorMessage = "El teléfono móvil es obligatorio.")]
        //[RegularExpression(@"^\+502\d{8}$", ErrorMessage = "El número de teléfono debe tener el formato +502 seguido de 8 dígitos.")]

        public string? TelefonoMovil { get; set; }

        public int IdSucursal { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public StatusUsuarioST? StatusUsuario { get; set; }
        public SucursalSC? Sucursal { get; set; }
        public GeneroGR? Genero { get; set; }
    }
}
