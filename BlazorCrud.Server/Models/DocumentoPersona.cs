using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class DocumentoPersona
{
    public int IdTipoDocumento { get; set; }

    public int IdPersona { get; set; }

    public string? NoDocumento { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;
}
