using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<RoleOpcion> RoleOpcions { get; set; } = new List<RoleOpcion>();

    public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; } = new List<UsuarioRole>();
}
