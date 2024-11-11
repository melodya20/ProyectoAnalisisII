using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Opcion
{
    public int IdOpcion { get; set; }

    public int IdMenu { get; set; }

    public string Nombre { get; set; } = null!;

    public int OrdenMenu { get; set; }

    public string Pagina { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Menu IdMenuNavigation { get; set; } = null!;

    public virtual ICollection<RoleOpcion> RoleOpcions { get; set; } = new List<RoleOpcion>();
}
