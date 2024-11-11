using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Nit { get; set; } = null!;

    public int? PasswordCantidadMayusculas { get; set; }

    public int? PasswordCantidadMinusculas { get; set; }

    public int? PasswordCantidadCaracteresEspeciales { get; set; }

    public int PasswordCantidadCaducidadDias { get; set; }

    public int? PasswordLargo { get; set; }

    public int? PasswordIntentosAntesDeBloquear { get; set; }

    public int? PasswordCantidadNumeros { get; set; }

    public int? PasswordCantidadPreguntasValidar { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();
}
