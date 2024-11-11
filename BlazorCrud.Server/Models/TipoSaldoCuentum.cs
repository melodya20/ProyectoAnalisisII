using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class TipoSaldoCuentum
{
    public int IdTipoSaldoCuenta { get; set; }

    public string? Nombre { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<SaldoCuentum> SaldoCuenta { get; set; } = new List<SaldoCuentum>();
}
