using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class TipoDocumento
{
    public int IdTipoDocumento { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<DocumentoPersona> DocumentoPersonas { get; set; } = new List<DocumentoPersona>();
}
