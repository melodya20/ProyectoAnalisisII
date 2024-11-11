using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class CierreMensualDTO
    {
        public DateTime FechaInicioMes { get; set; }
        public DateTime FechaFinMes { get; set; }
        public string Estado { get; set; } 
    }
}