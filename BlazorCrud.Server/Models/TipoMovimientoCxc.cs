using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class TipoMovimientoCxc
{
    public int IdTipoMovimientoCxc { get; set; }

    public string Nombre { get; set; } = null!;

    public int OperacionCuentaCorriente { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<MovimientoCuentum> MovimientoCuenta { get; set; } = new List<MovimientoCuentum>();
}
