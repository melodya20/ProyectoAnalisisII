using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int IdGenero { get; set; }

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? CorreoElectronico { get; set; }

    public int IdEstadoCivil { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<DocumentoPersona> DocumentoPersonas { get; set; } = new List<DocumentoPersona>();

    public virtual EstadoCivil IdEstadoCivilNavigation { get; set; } = null!;

    public virtual Genero IdGeneroNavigation { get; set; } = null!;

    public virtual ICollection<SaldoCuentum> SaldoCuenta { get; set; } = new List<SaldoCuentum>();
}
