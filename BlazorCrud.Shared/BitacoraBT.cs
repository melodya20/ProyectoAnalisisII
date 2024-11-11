using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class BitacoraBT
    {

        public int IdBitacoraAcceso { get; set; }

        public string IdUsuario { get; set; } = null!;

        public int IdTipoAcceso { get; set; }

        public DateTime FechaAcceso { get; set; }

        public string? HttpUserAgent { get; set; }

        public string? DireccionIp { get; set; }

        public string? Accion { get; set; }

        public string? SistemaOperativo { get; set; }

        public string? Dispositivo { get; set; }

        public string? Browser { get; set; }

        public string? Sesion { get; set; }

        public TipoAccesoTA? TipoAcceso { get; set; }
    }
}
