using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class EstadoCivil
{
    public int IdEstadoCivil { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
