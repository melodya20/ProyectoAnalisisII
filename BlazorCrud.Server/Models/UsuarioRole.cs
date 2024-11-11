using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class UsuarioRole
{
    public string IdUsuario { get; set; } = null!;

    public int IdRole { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
