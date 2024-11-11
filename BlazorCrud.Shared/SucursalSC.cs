using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class SucursalSC
    {
        public int IdSucursal { get; set; }

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public int IdEmpresa { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public EmpresaEM? Empresa { get; set; }
    }
}
