using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Modulo
{
    public int IdModulo { get; set; }

    public string Nombre { get; set; } = null!;

    public int OrdenMenu { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
