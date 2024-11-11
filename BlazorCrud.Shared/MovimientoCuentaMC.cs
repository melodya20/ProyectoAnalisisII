using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class MovimientoCuentaMC
    {
        public int IdMovimientoCuenta { get; set; }

        public int IdSaldoCuenta { get; set; }

        public int IdTipoMovimientoCxc { get; set; }

        public DateTime FechaMovimiento { get; set; }

        public decimal ValorMovimiento { get; set; }

        public decimal ValorMovimientoPagado { get; set; }

        public int GeneradoAutomaticamente { get; set; }

        public string Descripcion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public TipoMovCuentaCorrienteCC? movimiento { get; set; }

        public SaldoCuentumSC? saldo { get; set; }

    }
}
