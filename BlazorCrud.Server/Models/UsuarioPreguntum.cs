using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class UsuarioPreguntum
{
    public int IdPregunta { get; set; }

    public string IdUsuario { get; set; } = null!;

    public string Pregunta { get; set; } = null!;

    public string Respuesta { get; set; } = null!;

    public int OrdenPregunta { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
