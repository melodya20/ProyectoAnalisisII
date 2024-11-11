using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class SaldoCuentum
{
    public int IdSaldoCuenta { get; set; }

    public int IdPersona { get; set; }

    public int IdStatusCuenta { get; set; }

    public int IdTipoSaldoCuenta { get; set; }

    public decimal? SaldoAnterior { get; set; }

    public decimal? Debitos { get; set; }

    public decimal? Creditos { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual StatusCuentum IdStatusCuentaNavigation { get; set; } = null!;

    public virtual TipoSaldoCuentum IdTipoSaldoCuentaNavigation { get; set; } = null!;

    public virtual ICollection<MovimientoCuentum> MovimientoCuenta { get; set; } = new List<MovimientoCuentum>();
    public decimal SaldoActual
    {
        get { return (SaldoAnterior ?? 0) + (Debitos ?? 0) - (Creditos ?? 0); }
    }
}
