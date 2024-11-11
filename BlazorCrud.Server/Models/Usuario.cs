using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Usuario
{
    public string IdUsuario { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int IdStatusUsuario { get; set; }

    public byte[] Password { get; set; } = null!;

    public int IdGenero { get; set; }

    public DateTime? UltimaFechaIngreso { get; set; }

    public int? IntentosDeAcceso { get; set; }

    public string? SesionActual { get; set; }

    public DateTime UltimaFechaCambioPassword { get; set; }

    public string? CorreoElectronico { get; set; }

    public int? RequiereCambiarPassword { get; set; }

    public byte[]? Fotografia { get; set; }

    public string? TelefonoMovil { get; set; }

    public int IdSucursal { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Genero IdGeneroNavigation { get; set; } = null!;

    public virtual StatusUsuario IdStatusUsuarioNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioPreguntum> UsuarioPregunta { get; set; } = new List<UsuarioPreguntum>();

    public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; } = new List<UsuarioRole>();
}
