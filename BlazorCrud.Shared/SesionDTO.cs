using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class SesionDTO
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }

        public DateTime UltimaActividad { get; set; }
        public string HttpUserAgent { get; set; }
        public string DireccionIp { get; set; }
        public string SistemaOperativo { get; set; }
        public string Dispositivo { get; set; }
        public string Browser { get; set; }
        public string Sesion { get; set; }

    }
}
