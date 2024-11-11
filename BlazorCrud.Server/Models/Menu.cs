using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Menu
{
    public int IdMenu { get; set; }

    public int IdModulo { get; set; }

    public string Nombre { get; set; } = null!;

    public int OrdenMenu { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Modulo IdModuloNavigation { get; set; } = null!;

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();
}
